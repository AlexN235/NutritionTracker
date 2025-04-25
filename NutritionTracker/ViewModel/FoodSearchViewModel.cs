using CommunityToolkit.Mvvm.ComponentModel;


namespace NutruitionTracker.ViewModel;

using CommunityToolkit.Mvvm.Input;
using NutruitionTracker;
using NutruitionTracker.NutritionFacts;
using NutruitionTracker.NutritionFacts.Models;
using System.Reflection.Metadata;

public partial class FoodSearchViewModel : ObservableObject
{
    private const int QUERY_LIMIT = 10;
    NutritionDatabase db;

    public FoodSearchViewModel() 
    {
        db = new NutritionDatabase();
    }

    [ObservableProperty]
    string searchBar;

    [ObservableProperty]
    public List<FoodDisplay> searchResults;

    public List<FoodDisplay> getSearchResults(string s) 
    {
        return db.GetFood(s, QUERY_LIMIT);
    }


}
