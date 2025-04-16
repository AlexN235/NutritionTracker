using SQLite;
/// nutrient_name_id, nutrient_name, unit, nutrient_code

namespace NutruitionTracker.NutritionFacts.Models;
[Table("nutrient_name")]
class NutrientName
{
    [PrimaryKey]
    public int nutrient_code { get; set; }
    [NotNull]
    public int nutrient_name_id { get; set; }
    [NotNull]
    public string nutrient_name { get; set; }
    [NotNull]
    public int unit { get; set; }

}

