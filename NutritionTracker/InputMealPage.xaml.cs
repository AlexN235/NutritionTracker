using NutruitionTracker.NutritionFacts;
using NutruitionTracker.NutritionFacts.Models;
using NutruitionTracker.ViewModel;
using System.Collections.ObjectModel;

namespace NutruitionTracker;

public partial class InputMealPage : ContentPage
{

    public ObservableCollection<string> searchBarList;
    private NutritionDatabase database;
    public InputMealPage(InputMealViewModel vm, NutritionDatabase db)
	{
		InitializeComponent();
		BindingContext = vm;

        database = db;
        searchBarList = new ObservableCollection<string>();
	}

    void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        searchBarList.Clear();
        string s = ((SearchBar)sender).Text;
        List<String> f = database.GetFoodName(s);
        searchResults.ItemsSource = f;
    }

    void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e) {
        string select = (e.CurrentSelection.FirstOrDefault() as String);
        searchBar.Text = select;
    }

}