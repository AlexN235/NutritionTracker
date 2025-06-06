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
    async Task GoToAddMeal()
    {
        await Shell.Current.GoToAsync(nameof(InputMealPage));
        Task.Delay(100);
    }

    [RelayCommand]
    async Task OnSelectionGoToDetails() 
    {
        Dictionary<string, object> dict = new Dictionary<string, object>
        {
            ["Food"] = selectedItem
        };
        selectedItem = null;
        await Shell.Current.GoToAsync(nameof(DisplayDetailPage), dict);
    }

    private void DeleteMeal(FoodDisplay f) 
    {
        foreach (FoodDisplayGroup group in mealList.ToList()) 
        {
            for (int i = 0; i < group.Count; i++)
            {
                if (group[i].Equals(f)) {
                    group.RemoveAt(i);
                    UpdateList(group.Name);
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
        UpdateList(date.ToString("d"));
    }

    private void UpdateList(string date)
    {
        List<FoodDisplay> food_list = new List<FoodDisplay>();
        foreach (FoodDisplayGroup foodDisplays in mealList.ToList())
        {
            if (foodDisplays.Name == date) 
            {
                foreach (FoodDisplay foodDisplay in foodDisplays)
                {
                    food_list.Add(foodDisplay);
                }
            }
        }

        foreach (FoodDisplayGroup foodDisplays in mealList.ToList())
        {
            if (foodDisplays.Name == date)
            {
                mealList.Remove(foodDisplays);
                mealList.Add(new FoodDisplayGroup(date, food_list));
            }
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
            FoodDisplay q = query["toDelete"] as FoodDisplay;
            if (q == null)
                return;
            query.Clear();
            this.DeleteMeal(q);
        }
        else if (query.ContainsKey("Meal") != null)
        {
            FoodDisplay q = query["Meal"] as FoodDisplay;
            if (q == null)
                return;
            query.Clear();
            this.AddMeal(q);
        }
        else
            return;
        
    }


}

