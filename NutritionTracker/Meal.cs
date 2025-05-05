using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Microsoft.Maui.Controls;
using NutruitionTracker.NutritionFacts;
using NutruitionTracker.NutritionFacts.Models;

namespace NutruitionTracker;
public class Meal
{
    public String name { get; set; }
    public int totalWeight { get; set; }
    private int calories { get; set; }
    private int protein { get; set; }
    private int fibre { get; set; }
    private int carbohydrates { get; set; }
    private int sugar { get; set; }
    private int fat { get; set; }

    public Meal() { }

    public override string ToString()
    {
        return name;
    }

    public Meal(String name, int totalWeight, int calories, int protein, int fibre, int carbohydrates, int sugar, int fat) 
    {
        this.name = name;
        this.totalWeight = totalWeight;
        this.calories = calories;
        this.protein = protein;
        this.fibre = fibre;
        this.carbohydrates = carbohydrates;
        this.sugar = sugar;
        this.fat = fat;
    }
    public Meal(String name, int totalWeight)
    {
        this.name = name;
        this.totalWeight = totalWeight;
        this.calories = 0;
        this.protein = 0;
        this.fibre = 0;
        this.carbohydrates = 0;
        this.sugar = 0;
        this.fat = 0;
    }

    public void AddToMeal(List<string> ingrediantNames, List<int> ingrediantValues, int amountMultiplier) 
    {
        for (int i = 0; i < ingrediantNames.Count; i++)
            CheckAndAddNutrient(ingrediantNames[i], ingrediantValues[i] * amountMultiplier);
    }

    /* //////////////////////////////////////////////////////////////////////////////////////
    // Helper functions: determine if details in row contains a nutrient that is being using.
    */ //////////////////////////////////////////////////////////////////////////////////////
    private List<FoodNutritionDetail> GetFoodNutritionDetails(int food_id, NutritionDatabase db) 
    {
        // Grab Query
        string query = @"SELECT f.food_code, f.food_description, na.nutrient_value, nn.nutrient_name
                            FROM food as f
                                LEFT JOIN nutrient_amount AS na ON f.food_code = na.food_code
                                LEFT JOIN nutrient_name AS nn ON na.nutrient_name_id == nn.nutrient_name_id
                            WHERE f.food_code = '" + food_id + "'";

        List<FoodNutritionDetail> q = db.conn.Query<FoodNutritionDetail>(query).ToList();

        // return list of rows from query.
        return q.Select(x => new FoodNutritionDetail
        {
            food_code = x.food_code,
            food_description = x.food_description,
            nutrient_value = x.nutrient_value,
            nutrient_name = x.nutrient_name
        }).ToList();
    }
    private void CheckAndAddNutrient(string food_name, int amount)
    {
        switch (food_name)
        {
            case "Protein":
                this.protein += amount;
                break;
            case "Fat(total)":
                this.fat += amount;
                break;
            case "Carbohydrate":
                this.carbohydrates += amount;
                break;
            case "Energy(calories)":
                this.calories += amount;
                break;
            case "Fibre":
                this.fibre += amount;
                break;
            case "Sugars":
                this.sugar += amount;
                break;
        }
    }
}


/*  SELECT f.food_code, f.food_description, na.nutrient_value, nn.nutrient_name
    FROM food as f 
        LEFT JOIN nutrient_amount AS na ON f.food_code = na.food_code
        LEFT JOIN nutrient_name AS nn ON na.nutrient_name_id == nn.nutrient_name_id
    ORDER BY f.food_code, nn.nutrient_name
    WHERE f.food_code = "our food code";
*/

