using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    public List<string> GetFood(string name)
    {
        string sql = $"SELECT * FROM food WHERE food.food_description LIKE '%" + name + "%' LIMIT 5";
        List<Food> q = conn.Query<Food>(sql).ToList();
        List<string> res = new List<string>();

        foreach (Food f in q) 
        {
            res.Add(f.food_description);
        }

        return res;

    }
    
}

