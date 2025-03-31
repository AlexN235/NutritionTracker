using Microsoft.Graphics.Canvas.Svg;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutruitionTracker;
public class Meal
{
    public String name { get; set; }
    public int totalCalories { get; set; }

    public Meal() { }
    public Meal(String name, int totalCalories) 
    {
        this.name = name;
        this.totalCalories = totalCalories;
    }
    public Meal(String name, ObservableCollection allIngredients)
    {
        this.name = name;
        // do something with allIngredients to get all nutrition info.
    }
}

