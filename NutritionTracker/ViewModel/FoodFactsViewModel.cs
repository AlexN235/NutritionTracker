using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NutruitionTracker.NutritionFacts;

namespace NutruitionTracker.ViewModel;

public partial class FoodFactsViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    public string name;

    [ObservableProperty]
    public List<FoodDisplay> foodList;

    protected NutritionDatabase Database;

    public FoodFactsViewModel(NutritionDatabase db) 
    {
        this.Database = db;
        foodList = new List<FoodDisplay>();
        name = "Test Name";
    }

    [RelayCommand]
    public async Task Back()
    {
        await Shell.Current.GoToAsync("..");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.Count() == 0)
            return;
        FoodDisplay f = (FoodDisplay)query["Food"];
        SetInfo(f.Name);
    }

    private void SetInfo(string name)
    {
        this.Name = name;

        FoodItem f;
        try
        {
            f = new FoodItem(name, Database.GetFoodNutritionDetails(Database.GetClosestID(name)));
        }
        catch (InvalidPropertyForDatabaseQuery ex)
        {
            return;
        }
        List<string> names = f.ItemsNames;
        List<float> values = f.ItemsValue.ToList();

        FoodList = new List<FoodDisplay>();
        for (int i = 0; i < names.Count; i++)
            FoodList.Add(new FoodDisplay(names[i], values[i]));
    }
}
