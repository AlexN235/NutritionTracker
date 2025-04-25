using CommunityToolkit.Mvvm.ComponentModel;
using NutruitionTracker.ViewModel;

namespace NutruitionTracker;

public partial class FoodFacts : ContentPage
{
	string Name { get; set; }

	public FoodFacts() 
	{
		InitializeComponent();
		BindingContext = new FoodFactsViewModel();
	}

    public FoodFacts(string name)
    {
        InitializeComponent();
        BindingContext = new FoodFactsViewModel();
		Name = name;
    }

    public FoodFacts(FoodFactsViewModel vm, string name)
	{
		InitializeComponent();
		BindingContext = vm;
	}

}