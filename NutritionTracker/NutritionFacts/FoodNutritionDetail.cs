using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutruitionTracker.NutritionFacts;
class FoodNutritionDetail
{
    public int food_code { get; set; }
    public string food_description { get; set; }
    public int nutrient_value { get; set; }
    public string nutrient_name { get; set; }

    public FoodNutritionDetail() { }

    public FoodNutritionDetail(int food_code, string food_description, int nutrient_value, string nutrient_name) 
    {
        this.food_code = food_code;
        this.food_description = food_description;
        this.nutrient_value = nutrient_value;
        this.nutrient_name = nutrient_name;
    }
}

