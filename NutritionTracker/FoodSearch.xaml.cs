using CommunityToolkit.Mvvm.Input;
using NutruitionTracker.NutritionFacts;
using NutruitionTracker.ViewModel;
using System.Collections.ObjectModel;

namespace NutruitionTracker;

public partial class FoodSearch : ContentPage
{
    private const int QUERY_LIMIT = 10;
    ObservableCollection<FoodDisplay> SearchList;
    NutritionDatabase database;

    public FoodSearch(FoodSearchViewModel vm, NutritionDatabase db) 
    {
        InitializeComponent();
		BindingContext = vm;
        database = db;
        SearchList = new ObservableCollection<FoodDisplay>();
    }

    void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        SearchList.Clear();
        string s = ((SearchBar)sender).Text;
        List<FoodDisplay> f = database.GetFood(s, QUERY_LIMIT);
        SelectionCollection.ItemsSource = f;
    }

}