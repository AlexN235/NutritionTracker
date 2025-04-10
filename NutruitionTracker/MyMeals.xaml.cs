using NutruitionTracker.ViewModel;

namespace NutruitionTracker;

public partial class MyMeals : ContentPage
{
    public MyMeals(MyMealsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}