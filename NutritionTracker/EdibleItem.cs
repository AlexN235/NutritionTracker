using NutruitionTracker.NutritionFacts;

namespace NutruitionTracker;

public interface EdibleItem
{
    string Name { get; set; }
    NutritionDatabase database { get; set; }
    Dictionary<string, string> dbNamesTranslation { get; set; }
    List<string> itemsNames { get; set; }
    float[] itemsValue { get; set; }
}
