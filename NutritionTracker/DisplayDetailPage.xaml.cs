namespace NutruitionTracker;
using NutruitionTracker.ViewModel;
using System.Collections.Generic;

public partial class DisplayDetailPage : ContentPage
{

    DisplayDetailViewModel vm;

    public DisplayDetailPage(DisplayDetailViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}