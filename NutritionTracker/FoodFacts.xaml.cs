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
		this.vm = new FoodFactsViewModel(new NutritionFacts.NutritionDatabase());
		BindingContext = vm;
		head.Text = "Search to the top left.";
	}

    public FoodFacts(string name)
    {
        InitializeComponent();
		this.vm = new FoodFactsViewModel(new NutritionFacts.NutritionDatabase());
		vm.setName(name);
        BindingContext = vm;
        head.Text = name;
    }

    public FoodFacts(FoodFactsViewModel vm, string name)
	{
		InitializeComponent();
		BindingContext = vm;
	}

}