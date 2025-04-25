using SQLite;

namespace NutruitionTracker.NutritionFacts.Models;

[Table("food")]
public class Food
{
    [PrimaryKey]
    public int food_code { get; set; }

    [MaxLength(250)]
    public string food_description { get; set; }

}

