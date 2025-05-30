using NutruitionTracker.NutritionFacts;

namespace NutruitionTracker;

public class MealItem : EdibleItem, IEquatable<MealItem>
{
    public string Name { get; set; }
    public NutritionDatabase database { get; set; }
    public Dictionary<string, string> dbNamesTranslation { get; set; }
    public List<string> itemsNames { get; set; }
    public float[] itemsValue { get; set; }
    public DateTime Date { get; set; }

    public MealItem() {
        database = new NutritionDatabase() ;
        dbNamesTranslation = getDBToReadableDict();
        itemsNames = initializeItemNames();
        itemsValue = new float[itemsNames.Count];
        Name = "N/A";
        Date = DateTime.Today;
    }

    public void AddFoodsToFoodItem(List<FoodItem> food_list, List<float> weights)
    {
        for (int i = 0; i < food_list.Count; i++)
        {
            AddFoodToFoodItem(food_list[i], weights[i]);
        }
    }

    public void updateDate() { 
        Date = DateTime.Today;
    }

    public bool Equals(MealItem? other)
    {
        if (other == null) return false;
        int count = this.itemsValue.Count();
        if (other.itemsValue.Count() != count)
            return false;
        for (int i = 0; i < count; i++)
        {
            if (!compareFloatEqual(this.itemsValue.ElementAt(i), other.itemsValue.ElementAt(i)))
                return false;
        }
        return true;
    }

    private bool compareFloatEqual(float one, float two)
    {
        bool test = (Math.Abs(one - two) < 0.001);
        return test;
    }

    private void AddFoodToFoodItem(FoodItem f, float weight)
    {
        int id = database.GetClosestID(f.Name);
        AddFoodFromTable(id, weight);
    }

    private void AddFoodFromTable(int food_id, float weight)
    {
        List<FoodNutritionDetail> foodNutritionDetails = GetFoodNutritionDetails(food_id);
        foreach (FoodNutritionDetail detail in foodNutritionDetails)
            addData(detail, weight);
    }
    private Dictionary<string, string> getDBToReadableDict()
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
    private List<string> initializeItemNames()
    {
        return new List<string> { "Protein", "Fat(total)", "Carbohydrate", "Alcohol", "Energy(calories)", "Energy(kJ)",
                                  "Fibre", "Sugars",
                                  "Calcium", "Iron", "Magnesium", "Phosphorus", "Potassium", "Sodium", "Zinc", "Copper", "Manganese",
                                  "Vitamin B-6", "Vitamin B-12", "Vitamin C", "Vitamin D",
                                  "Cholesterol", "Caffeine"
                                };
    }
    private void addData(FoodNutritionDetail detail, float weight)
    {
        if (!dbNamesTranslation.ContainsKey(detail.nutrient_name))
            return;
        string name = dbNamesTranslation[detail.nutrient_name];

        int index = itemsNames.IndexOf(name);
        itemsValue[index] += detail.nutrient_value * weight / 100;
    }
    private List<FoodNutritionDetail> GetFoodNutritionDetails(int food_id)
    {
        // Grab Query
        string query = @"SELECT f.food_code, f.food_description, na.nutrient_value, nn.nutrient_name
                            FROM food as f
                                LEFT JOIN nutrient_amount AS na ON f.food_code = na.food_code
                                LEFT JOIN nutrient_name AS nn ON na.nutrient_name_id == nn.nutrient_name_id
                            WHERE f.food_code = '" + food_id + "'";

        List<FoodNutritionDetail> q = database.conn.Query<FoodNutritionDetail>(query).ToList();

        // return list of rows from query.
        return q.Select(x => new FoodNutritionDetail
        {
            food_code = x.food_code,
            food_description = x.food_description,
            nutrient_value = x.nutrient_value,
            nutrient_name = x.nutrient_name
        }).ToList();
    }
}
