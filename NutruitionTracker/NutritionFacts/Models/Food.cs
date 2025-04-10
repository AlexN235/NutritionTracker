using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace NutruitionTracker.NutritionFacts.Models;

[Table("food")]
class Food
{
    [PrimaryKey]
    public int food_code { get; set; }

    [MaxLength(250)]
    public string food_description { get; set; }

}

