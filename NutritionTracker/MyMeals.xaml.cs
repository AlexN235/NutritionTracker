using NutruitionTracker.NutritionFacts;
using NutruitionTracker.ViewModel;

namespace NutruitionTracker;

public partial class MyMeals : ContentPage
{
    private MyMealsViewModel VM;
    public MyMeals(MyMealsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        VM = vm;
    }

    public void AddMeal(FoodDisplay newMeal) 
    {
        VM.AddMeal(newMeal);
    }
}