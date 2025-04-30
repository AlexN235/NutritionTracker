using SQLite;
using NutruitionTracker.NutritionFacts.Models;

namespace NutruitionTracker.NutritionFacts;
public class NutritionDatabase
{
    public const string DB_NAME = @"\..\..\..\..\..\Properties\foodinfo.db";
    string path = System.Environment.ProcessPath + DB_NAME;
    public SQLiteConnection conn;
    public NutritionDatabase()
    {
        if (conn != null)
            return;

        conn = new SQLiteConnection(path);
        conn.CreateTable<Food>();
        conn.CreateTable<NutrientName>(); ///nutrient_name_id, nutrient_name, unit, nutrient_code
        conn.CreateTable<NutrientAmount>(); ///food_code, nutrient_value, nutrient_name_idsqlite
    }

    public List<string> GetFoodName(string name)
    {
        if (name == "")
            return new List<String>();

        List<Food> q = conn.Query<Food>(GetSQLQuery(name)).ToList();
        List<string> res = new List<string>();
        foreach (Food f in q) 
            res.Add(f.food_description);
        
        return res;
    }
    public List<FoodDisplay> GetFood(string name, int limit)
    {
        List<FoodDisplay> foodlist = new List<FoodDisplay>();
        if (name == "")
            return foodlist;

        List<Food> foods = conn.Query<Food>(GetSQLQuery(name), limit).ToList();
        foreach(Food f in foods) 
            foodlist.Add(new FoodDisplay(f.food_description));

        return foodlist;
    }

    public int GetClosestID(string s) 
    {
        if (s == "") return 100; /// Deal with error.

        List<Food> q = conn.Query<Food>(GetSQLQuery(s)).ToList();
        return q.First().food_code;
    }
    public string GetClosestName(string s)
    {
        if (s == "") return ""; /// Deal with error.

        List<Food> q = conn.Query<Food>(GetSQLQuery(s)).ToList();
        return q.First().food_description;
    }

    /* //////////////////////////////////////////////////////////////////
    /// Helper function: splits string (search bar) for database search
    */ //////////////////////////////////////////////////////////////////
    private static string[] SplitSearchbar(string s)
    {
        return s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    }
    private static string GetSQLQuery(string s)
    {
        string query = $"SELECT * FROM food WHERE";
        string[] words = SplitSearchbar(s);
        for (int i=0; i < words.Length; i++)
        {
            if (i != 0) 
                query += " AND";
            query += " food.food_description LIKE '%" + words[i] + "%'";
        }
        return query + "LIMIT 5";
    }
    private static string GetSQLQuery(string s, int limit)
    {
        string query = $"SELECT * FROM food WHERE";
        string[] words = SplitSearchbar(s);
        for (int i = 0; i < words.Length; i++)
        {
            if (i != 0)
                query += " AND";
            query += " food.food_description LIKE '%" + words[i] + "%'";
        }
        return query + "LIMIT " + limit;
    }



}

