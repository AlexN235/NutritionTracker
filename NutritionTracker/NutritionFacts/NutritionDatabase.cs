using NutruitionTracker.NutritionFacts.Models;
using SQLite;
using System.Collections.Generic;
using System.Diagnostics;

namespace NutruitionTracker.NutritionFacts;
public class NutritionDatabase
{
    private const string DB_NAME = @"\..\..\..\..\..\Properties\foodinfo.db";
    private string path = System.Environment.ProcessPath + DB_NAME;
    private SQLiteConnection conn;

    public NutritionDatabase()
    {
        if (conn != null)
            return;

        conn = new SQLiteConnection(path);
        conn.CreateTable<Food>();
        conn.CreateTable<NutrientName>();   // nutrient_name_id, nutrient_name, unit, nutrient_code
        conn.CreateTable<NutrientAmount>(); // food_code, nutrient_value, nutrient_name_idsqlite
    }

    public List<string> GetFoodName(string name)
    {
        List<string> res = new List<string>();
        if (name == "")
            return res;

        List<FoodDisplay> foods = getBestMatches(name);
        foreach (FoodDisplay f in foods) 
            res.Add(f.Name);
        
        return res;
    }

    public List<FoodDisplay> GetFood(string name, int limit = 5)
    {
        return getBestMatches(name);
    }

    private List<FoodDisplay> getBestMatches(string name, int limit = 5)
    {
        List<FoodDisplay> foodList = new List<FoodDisplay>();
        HashSet<FoodDisplay> temp = new HashSet<FoodDisplay>();
        if (name == "" || name.Count() < 2)
            return foodList;

        // Get queries that have exact match looking from start.
        string query = $"SELECT * FROM food WHERE food.food_description LIKE " + name + "%' LIMIT " + limit;
        List<Food> foods = conn.Query<Food>(GetSQLQuery(name), limit).ToList();
        foreach (Food f in foods)
            temp.Add(new FoodDisplay(f.food_description));

        // Get queries that prioritize first word that matching starting and has other matches
        string[] words = SplitSearchbar(name);
        if(words.Count() > 0) {
            query = $"SELECT * FROM food WHERE food.food_description LIKE '%" + words[0] + "%'";
            for (int i = 1; i < words.Length; i++)
            {
                query += " AND food.food_description LIKE '%" + words[i] + "%'";
            }
            query += " LIMIT " + limit;
            foods = conn.Query<Food>(GetSQLQuery(name), limit).ToList();
            foreach (Food f in foods)
                temp.Add(new FoodDisplay(f.food_description));
        }

        // Get queries for entries that contains any words
        query = $"SELECT * FROM food WHERE";
        for (int i = 0; i < words.Length; i++)
        {
            if (i != 0)
                query += " AND";
            query += " food.food_description LIKE '%" + words[i] + "%'";
        }
        query += " LIMIT " + limit;
        foods = conn.Query<Food>(GetSQLQuery(name), limit).ToList();
        foreach (Food f in foods) {
            temp.Add(new FoodDisplay(f.food_description));
        }

        return temp.ToList();
    }

    public int GetClosestID(string s) 
    {
        if (s == "")  throw new InvalidPropertyForDatabaseQuery("Unable to obtain database query with property"); // Deal with error.

        List<Food> q = conn.Query<Food>(GetSQLQuery(s)).ToList();
        if (q.Count == 0)
            throw new InvalidPropertyForDatabaseQuery();
        return q.First().food_code;
    }
    public string GetClosestName(string s)
    {
        if (s == "") throw new InvalidPropertyForDatabaseQuery();

        List<Food> q = conn.Query<Food>(GetSQLQuery(s)).ToList();
        if (q.Count == 0) 
            throw new InvalidPropertyForDatabaseQuery();

        return q.First().food_description;
    }

    public string GetNameWithID(int id)
    {
        if (id < 0) throw new InvalidPropertyForDatabaseQuery();

        List<Food> q = conn.Query<Food>(GetSQLQueryWithID(id)).ToList();
        if (q.Count == 0)
            throw new InvalidPropertyForDatabaseQuery();
        return q.First().food_description;
    }

    public List<FoodNutritionDetail> GetFoodNutritionDetails(int food_id)
    {
        // Grab Query
        string query = @"SELECT f.food_code, f.food_description, na.nutrient_value, nn.nutrient_name
                            FROM food as f
                                LEFT JOIN nutrient_amount AS na ON f.food_code = na.food_code
                                LEFT JOIN nutrient_name AS nn ON na.nutrient_name_id == nn.nutrient_name_id
                            WHERE f.food_code = '" + food_id + "'";

        List<FoodNutritionDetail> q = this.conn.Query<FoodNutritionDetail>(query).ToList();

        // return list of rows from query.
        return q.Select(x => new FoodNutritionDetail
        {
            food_code = x.food_code,
            food_description = x.food_description,
            nutrient_value = x.nutrient_value,
            nutrient_name = x.nutrient_name
        }).ToList();
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

    private static string GetSQLQueryWithID(int id)
    {
        return $"SELECT * FROM food WHERE food.food_code = '" + id + "'";
    }

}

