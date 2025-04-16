using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutruitionTracker;
public class Ingredient
{
    public String name { get; set; }
    public int amount { get; set; }
    public int database_food_id { get; set; }
    String measurementType { get; set; }

    public Ingredient(String name, string amount, int database_food_id)
    {
        measurementType = "g";
        this.name = name;
        this.amount = Int32.Parse(amount);
        this.database_food_id = database_food_id;
    }

    public override string ToString()
    {
        return name;
    }

}
