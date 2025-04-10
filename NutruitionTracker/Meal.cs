using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace NutruitionTracker;
public class Meal
{
    public int id { get; set; }
    public String name { get; set; }
    public int totalCalories { get; set; }

    public Meal() { }
    public Meal(String name, int totalCalories) 
    {
        this.name = name;
        this.totalCalories = totalCalories;
    }
    public Meal(String name, ObservableCollection<Ingredient> allIngredients)
    {
        this.name = name;
        // do something with allIngredients to get all nutrition info.
    }
}

