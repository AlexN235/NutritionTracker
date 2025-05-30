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

    private NutritionDatabase database;

    public FoodFactsViewModel(NutritionDatabase db) 
    {
        this.database = db;
        foodList = new List<FoodDisplay>();
    }

    public void setName(string name) {
        this.name = name;

        FoodItem f = new FoodItem(name, database.GetFoodNutritionDetails(database.GetClosestID(name)));
        List<string> names = f.itemsNames;
        List<float> values = f.itemsValue.ToList();

        List<FoodDisplay> temp = new List<FoodDisplay>();
        for (int i = 0; i < names.Count; i++)
            temp.Add(new FoodDisplay(names[i], values[i]));

        FoodList = temp;
    }

    [RelayCommand]
    public async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        FoodDisplay f = (FoodDisplay)query["Food"];
        setName(f.Name);
    }
}
