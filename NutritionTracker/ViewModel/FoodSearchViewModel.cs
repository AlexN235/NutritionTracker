using CommunityToolkit.Mvvm.ComponentModel;
namespace NutruitionTracker.ViewModel;

using CommunityToolkit.Mvvm.Input;
using NutruitionTracker;
using NutruitionTracker.NutritionFacts;
using NutruitionTracker.NutritionFacts.Models;
using System.Reflection.Metadata;

public partial class FoodSearchViewModel : ObservableObject
{
    [ObservableProperty]
    string searchBar;
    [ObservableProperty]
    FoodDisplay selectedItem;
    [ObservableProperty]
    public List<FoodDisplay> searchResults;

    public FoodSearchViewModel() { }

    [RelayCommand]
    async Task GoToFoodFacts() 
    {
        if (selectedItem == null)
            return;

        Dictionary<string, object> dict = new Dictionary<string, object>
        {
            ["Food"] = selectedItem
        };
        await Shell.Current.GoToAsync(nameof(FoodFacts), dict);
    }
}
