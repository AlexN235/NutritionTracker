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
    public const string DB_NAME = "foodinfo.db";
    public SQLiteConnection conn;
    public NutritionDatabase()
    {
        if (conn != null)
            return;

        conn = new SQLiteConnection(DB_NAME);
        conn.CreateTable<Food>();
    }

    public List<string> GetFood(string name)
    {
        string sql = $"SELECT * FROM food LIMIT 5";
        List<Food> q = conn.Query<Food>(sql).ToList();
        List<string> res = new List<string>();

        return res;

    }
    
}

