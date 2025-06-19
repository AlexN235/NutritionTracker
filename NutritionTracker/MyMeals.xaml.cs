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

    public void ChartTest()
    {
        ChartEntry[] entries = new[]
        {
            new ChartEntry(123)
            {
                Label = "Food1",
                ValueLabel = "123",
                Color = SKColor.Parse("#2c3e60")
            },
            new ChartEntry(90)
            {
                Label = "Food2",
                ValueLabel = "90",
                Color = SKColor.Parse("#2c3e60")
            },
            new ChartEntry(290)
            {
                Label = "Food3",
                ValueLabel = "290",
                Color = SKColor.Parse("#2c3e60")
            },
            new ChartEntry(220)
            {
                Label = "Food4",
                ValueLabel = "220",
                Color = SKColor.Parse("#2c3e60")
            }
        };
        mealsChart.Chart = new LineChart { Entries = entries };
    }
}