using CommunityToolkit.Mvvm.ComponentModel;
using NutruitionTracker.ViewModel;
using System.Runtime.CompilerServices;

namespace NutruitionTracker;

public partial class FoodFacts : ContentPage
{
	FoodFactsViewModel vm;
	public FoodFacts(FoodFactsViewModel vm) 
	{
		InitializeComponent();
		BindingContext = vm;
		head.Text = "Search to the top left.";
	}

    public FoodFacts(FoodFactsViewModel vm, string name)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}