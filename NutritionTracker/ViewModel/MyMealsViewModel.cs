using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NutruitionTracker.NutritionFacts;
using System.Collections.ObjectModel;

namespace NutruitionTracker.ViewModel;

public partial class MyMealsViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<Meal> mealList = new ObservableCollection<Meal>();

    private void breakpoint() { return; }

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

    public void AddMeal(Meal newMeal) 
    {
        mealList.Add(newMeal);
    }

}

