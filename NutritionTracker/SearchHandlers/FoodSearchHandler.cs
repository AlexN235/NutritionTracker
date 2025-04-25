using CommunityToolkit.Mvvm.ComponentModel;
using NutruitionTracker.NutritionFacts;


namespace NutruitionTracker.SearchHandlers;


class FoodSearchHandler : SearchHandler
{
    public IList<String> Foods { get; set; }
    NutritionDatabase db;

    public FoodSearchHandler()
    {
        Foods = new List<String>();
        db = new NutritionDatabase();
    }

    protected override void OnQueryChanged(string oldValue, string newValue)
    {
        base.OnQueryChanged(oldValue, newValue);

        if(string.IsNullOrWhiteSpace(newValue))
        {
            ItemsSource = null;
        }
        else
        {
            List<String> f = db.GetFoodName(newValue);
            ItemsSource = f;
        }
    }

    protected override void OnItemSelected(object item)
    {
        base.OnItemSelected(item);
    }
}
