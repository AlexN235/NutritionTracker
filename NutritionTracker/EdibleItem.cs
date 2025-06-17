using NutruitionTracker.NutritionFacts;

namespace NutruitionTracker;

public interface EdibleItem
{
    string Name { get; set; }
    NutritionDatabase Database { get; set; }
    Dictionary<string, string> DatabaseNamesTranslation { get; set; }
    List<string> ItemsNames { get; set; }
    float[] ItemsValue { get; set; }
}
