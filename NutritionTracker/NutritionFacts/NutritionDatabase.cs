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

    public List<string> GetFood(string name)
    {
        if (name == "")
            return new List<String>();

        List<Food> q = conn.Query<Food>(GetSQLQuery(name)).ToList();
        List<string> res = new List<string>();
        foreach (Food f in q) 
            res.Add(f.food_description);
        
        return res;

    }

    public int GetClosestID(string s) 
    {
        if (s == "") return -1; /// Deal with error.

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

}

