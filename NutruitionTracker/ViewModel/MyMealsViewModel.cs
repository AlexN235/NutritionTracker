using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Graphics.Canvas.Text;
using NutruitionTracker.NutritionFacts;

namespace NutruitionTracker.ViewModel;

[QueryProperty(nameof(NutritionDatabase), "NutritionDB")]
public partial class MyMealsViewModel : ObservableObject
{

    [ObservableProperty]
    NutritionDatabase db;
    public MyMealsViewModel() 
    {
        db = Db;
        breakpause();
    }

    public void breakpause() 
    {
        return;
    }

    [RelayCommand]
    async Task GoBack() 
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async Task AddMeal()
    {

        await Shell.Current.GoToAsync(nameof(InputMealPage),
            new Dictionary<string, object>
            {
                ["NutritionDB"] = db
            });
    }
}

