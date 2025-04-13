using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NutruitionTracker.NutritionFacts;

namespace NutruitionTracker.ViewModel;

public partial class MyMealsViewModel : ObservableObject
{
    public MyMealsViewModel() 
    {

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
        await Shell.Current.GoToAsync(nameof(InputMealPage));
    }
}

