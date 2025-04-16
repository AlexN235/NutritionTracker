using SQLite;

namespace NutruitionTracker.NutritionFacts.Models;
[Table("nutrient_amount")]
class NutrientAmount
{
    [NotNull]
    public int nutrient_name_id { get; set; }
    [NotNull]
    public int food_code { get; set; }
    [NotNull]
    public int nutrient_value { get; set; }
}
