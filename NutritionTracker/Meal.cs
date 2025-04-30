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
    private int saturated_fat { get; set; }
    private int unsaturated_fat { get; set; }

    public Meal() { }

    public override string ToString()
    {
        return name;
    }

    public Meal(String name, int totalWeight, int calories, int protein, int fibre, int carbohydrates, int sugar, int saturated_fat, int unsaturated_fat) 
    {
        this.name = name;
        this.totalWeight = totalWeight;
        this.calories = calories;
        this.protein = protein;
        this.fibre = fibre;
        this.carbohydrates = carbohydrates;
        this.sugar = sugar;
        this.saturated_fat = saturated_fat;
        this.unsaturated_fat = unsaturated_fat;
    }
    public Meal(String name, int totalWeight, List<Ingredient> allIngredients, NutritionDatabase db)
    {
        this.name = name;
        this.totalWeight = totalWeight;
        this.calories = 0;
        this.protein = 0;
        this.fibre = 0;
        this.carbohydrates = 0;
        this.sugar = 0;
        this.saturated_fat = 0;
        this.unsaturated_fat = 0;

        foreach (Ingredient i in allIngredients)
        {
            List<FoodNutritionDetail> foodNutritionDetails = GetFoodNutritionDetails(i.database_food_id, db);
            foreach (FoodNutritionDetail detail in foodNutritionDetails)
                CheckAndAddNutrient(detail, i.amount);
        }
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
    private void CheckAndAddNutrient(FoodNutritionDetail detail, int amount)
    {
        if (detail.nutrient_name == "CARBOHYDRATE, TOTAL (BY DIFFERENCE)")
            this.carbohydrates += detail.nutrient_value * amount;
        if (detail.nutrient_name == "SUGAR")
            this.sugar += detail.nutrient_value * amount;
        if (detail.nutrient_name == "SUCROSE")
            this.sugar += detail.nutrient_value * amount;
        if (detail.nutrient_name == "PROTIEN")
            this.protein += detail.nutrient_value * amount;
        if (detail.nutrient_name == "FIBRE, TOTAL DIETARY")
            this.fibre += detail.nutrient_value * amount;
        if (detail.nutrient_name == "ENERGY (KILOCALORIES)")
            this.calories += detail.nutrient_value * amount;
        if (detail.nutrient_name == "FATTY ACIDS, POLYUNSATURATED, TOTAL")
            this.unsaturated_fat += detail.nutrient_value * amount;
        if (detail.nutrient_name == "FATTY ACIDS, MONOUNSATURATED, TOTAL")
            this.unsaturated_fat += detail.nutrient_value * amount;
        if (detail.nutrient_name == "FATTY ACIDS, TRANS, TOTAL")
            this.unsaturated_fat += detail.nutrient_value * amount;
        if (detail.nutrient_name == "FATTY ACIDS, SATURATED, TOTAL")
            this.saturated_fat += detail.nutrient_value * amount;
    }
}


/*  SELECT f.food_code, f.food_description, na.nutrient_value, nn.nutrient_name
    FROM food as f 
        LEFT JOIN nutrient_amount AS na ON f.food_code = na.food_code
        LEFT JOIN nutrient_name AS nn ON na.nutrient_name_id == nn.nutrient_name_id
    ORDER BY f.food_code, nn.nutrient_name
    WHERE f.food_code = "our food code";
*/

