using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NutruitionTracker.NutritionFacts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NutruitionTracker.ViewModel;

public partial class MyMealsViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    private ObservableCollection<FoodDisplayGroup> mealList;
    [ObservableProperty]
    private FoodDisplay selectedItem;

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

    [RelayCommand]
    async Task OnSelectionGetDetails() 
    {
        await Shell.Current.GoToAsync(nameof(DisplayDetailPage), new Dictionary<string, object>
        {
            ["foodID"] = selectedItem
        });
    }

    private void DeleteMeal(FoodDisplay f) 
    {
        foreach (FoodDisplayGroup group in mealList) 
        {
            for (int i = 0; i < group.Count; i++)
            {
                if (group[i].Equals(f)) {
                    group.RemoveAt(i);
                }
            }
        } 
    }

    private void AddMeal(FoodDisplay newMeal) 
    {
        DateTime date = newMeal.GetDate();
        var group = mealList.Where(x => x.Name == date.ToString("d"));
        if (group.Any())
        {
            mealList.First(x => x.Name == date.ToString("d")).AddToList(newMeal);
        }
        else 
        {
            List<FoodDisplay> temp = [newMeal];
            mealList.Add(new FoodDisplayGroup(date.ToString("d"), temp));
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.Count == 0) 
        {
            return;
        }
        else if (query.ContainsKey("toDelete"))
        {
            this.DeleteMeal(query["toDelete"] as FoodDisplay);
        }
        else if (query.ContainsKey("Meal") != null)
        {
            this.AddMeal(query["Meal"] as FoodDisplay);
        }
    }

}

