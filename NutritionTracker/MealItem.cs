using NutruitionTracker.NutritionFacts;
using System.Xml.Linq;

namespace NutruitionTracker;

public class MealItem : EdibleItem, IEquatable<MealItem>
{
    public string Name { get; set; }
    public NutritionDatabase Database { get; set; }
    public Dictionary<string, string> DatabaseNamesTranslation { get; set; }
    public List<string> ItemsNames { get; set; }
    public float[] ItemsValue { get; set; }
    public DateTime Date { get; set; }

    public MealItem() {
        Database = new NutritionDatabase() ;
        DatabaseNamesTranslation = getDatabaseToReadableDictionary();
        ItemsNames = initializeItemNames();
        ItemsValue = new float[ItemsNames.Count];
        Name = "N/A";
        Date = DateTime.Today;
    }
    public MealItem(float[] values, string name, DateTime date)
    {
        Database = new NutritionDatabase();
        DatabaseNamesTranslation = getDatabaseToReadableDictionary();
        ItemsNames = initializeItemNames();
        ItemsValue = values;
        Name = name;
        Date = date;
    }

    public void AddFoodsToMeal(List<FoodItem> food_list, List<float> weights)
    {
        for (int i = 0; i < food_list.Count; i++)
        {
            AddFood(food_list[i], weights[i]);
        }
    }
    public bool Equals(MealItem? other)
    {
        if (other == null) return false;
        int count = this.ItemsValue.Count();
        if (other.ItemsValue.Count() != count)
            return false;
        for (int i = 0; i < count; i++)
        {
            if (!compareFloatEqual(this.ItemsValue.ElementAt(i), other.ItemsValue.ElementAt(i)))
                return false;
        }
        return true;
    }

    public float GetCalories()
    {
        return ItemsValue[4];
    }



    // #################################### Helper Functions ####################################

    private bool compareFloatEqual(float num1, float num2)
    {
        return Math.Abs(num1 - num2) < 0.001;
    }
    private void AddFood(FoodItem f, float weight)
    {
        try 
        { 
            AddFoodFromDatabase(Database.GetClosestID(f.Name), weight);
        }
        catch (InvalidPropertyForDatabaseQuery ex) 
        { 
            return;
        }
    }
    private void AddFoodFromDatabase(int food_id, float weight)
    {
        List<FoodNutritionDetail> foodNutritionDetails = Database.GetFoodNutritionDetails(food_id);
        foreach (FoodNutritionDetail detail in foodNutritionDetails)
            addData(detail, weight);
    }
    private void addData(FoodNutritionDetail detail, float weight)
    {
        if (!DatabaseNamesTranslation.ContainsKey(detail.nutrient_name))
            return;

        string name = DatabaseNamesTranslation[detail.nutrient_name];
        ItemsValue[ItemsNames.IndexOf(name)] += detail.nutrient_value * weight / 100;
    }
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
}
