using Microcharts;
using NutruitionTracker.NutritionFacts;
using NutruitionTracker.ViewModel;
using SkiaSharp;

namespace NutruitionTracker;

public partial class MyMeals : ContentPage
{
    public MyMeals(MyMealsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}