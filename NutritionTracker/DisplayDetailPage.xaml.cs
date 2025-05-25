namespace NutruitionTracker;
using NutruitionTracker.ViewModel;
using System.Collections.Generic;

public partial class DisplayDetailPage : ContentPage, IQueryAttributable
{

    DisplayDetailViewModel vm;

    public DisplayDetailPage()
	{
		InitializeComponent();
        vm = new DisplayDetailViewModel();
        BindingContext = vm;
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        var q = query["foodID"] as FoodDisplay;
        vm.Set(q);
    }
}