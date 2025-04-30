using CommunityToolkit.Mvvm.ComponentModel;
using NutruitionTracker.ViewModel;
using System.Runtime.CompilerServices;

namespace NutruitionTracker;

public partial class FoodFacts : ContentPage
{
	FoodFactsViewModel vm;
	public FoodFacts() 
	{
		InitializeComponent();
		vm = new FoodFactsViewModel();
		BindingContext = vm;
		head.Text = "Search to the top left.";
	}

    public FoodFacts(string name)
    {
        InitializeComponent();
        vm = new FoodFactsViewModel();
        BindingContext = new FoodFactsViewModel(name);
        head.Text = name;
    }

    public FoodFacts(FoodFactsViewModel vm, string name)
	{
		InitializeComponent();
		BindingContext = vm;
	}

}