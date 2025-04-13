using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutruitionTracker;
public class Ingredient
{
    public String name { get; set; }
    public String amount { get; set; }
    String measurementType { get; set; }

    public Ingredient() {
        measurementType = "g";
    }
    public Ingredient(String name, String amount)
    {
        this.name = name;
        this.amount = amount;
        measurementType = "g";
    }

    public override string ToString()
    {
        return name;
    }

}
