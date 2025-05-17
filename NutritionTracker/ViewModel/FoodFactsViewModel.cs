using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace NutruitionTracker.ViewModel;

public partial class FoodFactsViewModel : ObservableObject
{
    [ObservableProperty]
    public string name;

    [ObservableProperty]
    public List<FoodDisplay> foodList;

    public FoodFactsViewModel() 
    {
        foodList = new List<FoodDisplay>();
    }

    public FoodFactsViewModel(string name) 
    {
        this.name = name;

        FoodItem f = new FoodItem(name);
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
}
