using NutruitionTracker.NutritionFacts.Models;
using NutruitionTracker.ViewModel;
using System.Collections.ObjectModel;

namespace NutruitionTracker;

public partial class InputMealPage : ContentPage
{

    ObservableCollection<string> searchBarList;
    InputMealViewModel viewmodel;
    public InputMealPage(InputMealViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

        viewmodel = vm;
        searchBarList = new ObservableCollection<string>();
	}

    void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        searchBarList.Clear();
        string s = ((SearchBar)sender).Text;
        List<String> f = viewmodel.GetFromFood(s);
        searchResults.ItemsSource = f.Take(5);
    }
}