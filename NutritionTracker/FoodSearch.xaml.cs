using CommunityToolkit.Mvvm.Input;
using NutruitionTracker.ViewModel;

namespace NutruitionTracker;

public partial class FoodSearch : ContentPage
{
    FoodSearchViewModel vm;

    public FoodSearch() 
    {
        InitializeComponent();
        this.vm = new FoodSearchViewModel();
		BindingContext = this.vm;
    }

    public CollectionView GetSelectionCollection()
    {
        return selectionCollection;
    }

	void SearchBar_TextChanged(object sender, TextChangedEventArgs e) 
	{
		string s = ((SearchBar)sender).Text;
		List<FoodDisplay> r = vm.getSearchResults(s);
        selectionCollection.ItemsSource = r;
    }

}