using NutruitionTracker.NutritionFacts;

namespace NutruitionTracker;

public class FoodItem
{
    public string Name { get; set; }
    private NutritionDatabase database;
    private Dictionary<string, string> dbNamesTranslation;
    private List<string> itemsNames;
    private float[] itemsValue;
    private int[] shortListIndex;

    public FoodItem()
    {
        database = new NutritionDatabase();
        dbNamesTranslation = getDBToReadableDict();
        itemsNames = initializeItemNames();
        itemsValue = new float[itemsNames.Count];
        shortListIndex = new[] {2, 7, 0, 6, 4, 1};
        Name = "N/A";
    }

    public FoodItem(string query) 
    {
        database = new NutritionDatabase();
        dbNamesTranslation = getDBToReadableDict();
        itemsNames = initializeItemNames();
        itemsValue = new float[itemsNames.Count];
        shortListIndex = new[] { 2, 7, 0, 6, 4, 1 };
        Name = database.GetClosestName(query);

        List<FoodNutritionDetail> foodNutritionDetails = GetFoodNutritionDetails(database.GetClosestID(query));
        foreach (FoodNutritionDetail detail in foodNutritionDetails)
            addData(detail);
    }

    public FoodItem(int id)
    {
        database = new NutritionDatabase();
        dbNamesTranslation = getDBToReadableDict();
        itemsNames = initializeItemNames();
        itemsValue = new float[itemsNames.Count];
        shortListIndex = new[] { 2, 7, 0, 6, 4, 1 };
        Name = database.GetNameWithID(id);
        
        List<FoodNutritionDetail> foodNutritionDetails = GetFoodNutritionDetails(id);
        foreach (FoodNutritionDetail detail in foodNutritionDetails)
            addData(detail);
    }

    public List<string> getItemsNames() { return itemsNames; }

    public List<float> getItemValues() { return itemsValue.ToList(); }

    public List<string> getItemNamesShort() 
    {
        List<string> shortList = new List<string>();
        foreach (int i in shortListIndex)
            shortList.Add(itemsNames[i]);
        return shortList; 
    }

    public List<float> getItemValuesShort()
    {
        List<float> shortList = new List<float>();
        foreach (int i in shortListIndex)
            shortList.Add(itemsValue[i]);
        return shortList;
    }

    public void AddFoodsToFoodItem(List<FoodItem> food_list, List<float> weights) 
    {
        float totalWeight = 0;
        foreach (float f in weights)
            totalWeight += f;
        for (int i = 0; i < food_list.Count; i++)
        {
            AddFoodToFoodItem(food_list[i], weights[i], totalWeight);
        }
    }
    public void AddFoodToFoodItem(FoodItem f, float weight, float totalWeight) 
    {
        int id = database.GetClosestID(f.Name);
        AddFoodFromTable(id, weight, totalWeight);
    }

    public void AddFoodFromTable(int food_id) 
    {
        List<FoodNutritionDetail> foodNutritionDetails = GetFoodNutritionDetails(food_id);
        foreach (FoodNutritionDetail detail in foodNutritionDetails)
            addData(detail);
    }
    public void AddFoodFromTable(int food_id, float weight, float totalWeight)
    {
        List<FoodNutritionDetail> foodNutritionDetails = GetFoodNutritionDetails(food_id);
        foreach (FoodNutritionDetail detail in foodNutritionDetails)
            addData(detail, weight, totalWeight);
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

    private void addData(FoodNutritionDetail detail) 
    {
        if (!dbNamesTranslation.ContainsKey(detail.nutrient_name))
            return;
        string name = dbNamesTranslation[detail.nutrient_name];

        int index = itemsNames.IndexOf(name);
        itemsValue[index] += detail.nutrient_value;
    }

    private void addData(FoodNutritionDetail detail, float weight, float totalWeight)
    {
        if (!dbNamesTranslation.ContainsKey(detail.nutrient_name))
            return;
        string name = dbNamesTranslation[detail.nutrient_name];

        int index = itemsNames.IndexOf(name);
        itemsValue[index] += detail.nutrient_value * weight / totalWeight;
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

/* Proxiamtes:
    * Protein
    * Total Fat
    * Carbohydrate
    * Alchohol
    * Energy (kcal)
    * Energy (kj)
    * 
    * Other Carbohydates:
    * Fibre
    * Sugar
    * 
* Minerals:
    * Calcium, Ca
    * Iron, Fe
    * Magnesium, Mg
    * Phosphorus, P
    * Potassium, K
    * Zinc, Zn
    * Copper, Cu
    * Manganese, Mn
    * Selenium, Se
    * 
* Vitamins:
    * Vitamin B-6
    * Vitamin B-12
    * Vitamin C
    * Vitamin D
    * Vitamin D (IU)
    * 
* Fats:
    * 
* Others:
    * Caffeine
    */
