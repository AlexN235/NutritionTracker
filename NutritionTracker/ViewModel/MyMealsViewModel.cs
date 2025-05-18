using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NutruitionTracker.NutritionFacts;
using System.Collections.ObjectModel;

namespace NutruitionTracker.ViewModel;

public partial class MyMealsViewModel : ObservableObject
{
    [ObservableProperty]
    ObservableCollection<FoodDisplayGroup> mealList;

    public MyMealsViewModel() 
    {
        mealList = new ObservableCollection<FoodDisplayGroup>();
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

    public void AddMeal(FoodDisplay newMeal) 
    {
        DateTime date = newMeal.GetDate();
        var group = mealList.Where(x => x.Name == date.ToString("d"));
        if (group.Any())
        {
            //List<FoodDisplay> temp = group.First();
            //temp.Add(newMeal);
            //mealList.Add(new FoodDisplayGroup(date.ToString("d"), temp));
            mealList.First(x => x.Name == date.ToString("d")).AddToList(newMeal);
        }
        else 
        {
            List<FoodDisplay> temp = new List<FoodDisplay>();
            temp.Add(newMeal);
            mealList.Add(new FoodDisplayGroup(date.ToString("d"), temp));
        }

    }

}

