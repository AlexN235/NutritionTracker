using NutruitionTracker.NutritionFacts;

namespace NutruitionTracker;

public class FoodItem : EdibleItem, IEquatable<FoodItem>
{
    public string Name { get; set; }
    public NutritionDatabase Database { get; set; }
    public Dictionary<string, string> DatabaseNamesTranslation { get; set; }
    public List<string> ItemsNames { get; set; }
    public float[] ItemsValue { get; set; } 

    public FoodItem()
    {
        Database = new NutritionDatabase();
        DatabaseNamesTranslation = getDatabaseToReadableDictionary();
        ItemsNames = initializeItemNames();
        ItemsValue = new float[ItemsNames.Count];
        Name = "N/A";
    }

    public FoodItem(String name, List<FoodNutritionDetail> foodNutritionDetails)
    {
        Database = new NutritionDatabase();
        DatabaseNamesTranslation = getDatabaseToReadableDictionary();
        ItemsNames = initializeItemNames();
        ItemsValue = new float[ItemsNames.Count];
        Name = name;

        foreach (FoodNutritionDetail detail in foodNutritionDetails)
            addData(detail);
    }

    public bool Equals(FoodItem? other)
    {
        if (other is null) return false;
        if(other.ItemsValue.Count() != this.ItemsValue.Count()) return false;

        for (int i = 0; i< this.ItemsValue.Count(); i++) 
        {
            if (!compareFloatEqual(this.ItemsValue.ElementAt(i), other.ItemsValue.ElementAt(i)))
                return false;
        }
        return true;
    }



    // #################################### Helper Functions ####################################

    private List<string> initializeItemNames()
    {
        return new List<string> { "Protein", "Fat(total)", "Carbohydrate", "Alcohol", "Energy(calories)", "Energy(kJ)",
                                  "Fibre", "Sugars",
                                  "Calcium", "Iron", "Magnesium", "Phosphorus", "Potassium", "Sodium", "Zinc", "Copper", "Manganese",
                                  "Vitamin B-6", "Vitamin B-12", "Vitamin C", "Vitamin D",
                                  "Cholesterol", "Caffeine"
                                };
    }
    private Dictionary<string, string> getDatabaseToReadableDictionary()
    {
        var keys = new List<string> { "PROTEIN", "FATTY ACIDS, POLYUNSATURATED, TOTAL", "FATTY ACIDS, MONOUNSATURATED, TOTAL", "FATTY ACIDS, TRANS, TOTAL" , "FATTY ACIDS, SATURATED, TOTAL", "CARBOHYDRATE, TOTAL (BY DIFFERENCE)", "ALCOHOL", "ENERGY (KILOCALORIES)", "ENERGY (KILOJOULES)",
                                      "FIBRE, TOTAL DIETARY", "SUGARS, TOTAL", "SUCROSE",
                                      "CALCIUM", "IRON", "MAGNESIUM", "PHOSPHORUS", "POTASSIUM", "SODIUM", "ZINC", "COPPER", "MANGANESE",
                                      "VITAMIN B-6", "VITAMIN B-12", "VITAMIN C", "VITAMIN D (D2 + D3)",
                                      "CHOLESTEROL", "CAFFEINE"
                                    };
        var values = new List<string> { "Protein", "Fat(total)", "Fat(total)", "Fat(total)", "Fat(total)", "Carbohydrate", "Alcohol", "Energy(calories)", "Energy(kJ)",
                                        "Fibre", "Sugars", "Sugars",
                                        "Calcium", "Iron", "Magnesium", "Phosphorus", "Potassium", "Sodium", "Zinc", "Copper", "Manganese",
                                        "Vitamin B-6", "Vitamin B-12", "Vitamin C", "Vitamin D",
                                        "Cholesterol", "Caffeine"
                                      };

        return keys.Zip(values, (k, v) => new { Key = k, Value = v }).ToDictionary(x => x.Key, x => x.Value);
    }
    private void addData(FoodNutritionDetail detail) 
    {
        if (!DatabaseNamesTranslation.ContainsKey(detail.nutrient_name))
            return;
        string name = DatabaseNamesTranslation[detail.nutrient_name];

        int index = ItemsNames.IndexOf(name);
        ItemsValue[index] += detail.nutrient_value;
    }
    private bool compareFloatEqual(float one, float two)
    {
        return (Math.Abs(one - two) < 0.001);
    }
}
