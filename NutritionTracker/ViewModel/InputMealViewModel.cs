using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

using NutruitionTracker.NutritionFacts;
namespace NutruitionTracker.ViewModel;

public partial class InputMealViewModel : ObservableObject
{
    private NutritionDatabase Database;

    [ObservableProperty]
    ObservableCollection<FoodDisplay> foods;
    [ObservableProperty]
    string ingrediantText;
    [ObservableProperty]
    string weightText;
    [ObservableProperty]
    string mealName;
    [ObservableProperty]
    private string query;
    [ObservableProperty]
    private List<String> searchResult;
    [ObservableProperty]
    FoodDisplay selectedItem;

    public InputMealViewModel(NutritionDatabase db)
    {
        foods = new ObservableCollection<FoodDisplay>();
        this.Database = db;
    }

    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    // Add to list of ingrediants in the meal.
    [RelayCommand]
    void Add() 
    {
        if (string.IsNullOrWhiteSpace(IngrediantText) || string.IsNullOrWhiteSpace(WeightText))
            return;
        // TO DO: Check for WeightText to be an numbers

        // Add selected item, if no selection made add top on list choice.
        FoodDisplay display;
        if (SelectedItem != null) 
        {
            display = SelectedItem;
        }
        else 
        {
            string name = Database.GetClosestName(IngrediantText);
            // POSSIBLE ERROR: If no closest choice can be made.
            FoodItem newItem = new FoodItem(name, Database.GetFoodNutritionDetails(Database.GetClosestID(name)));
            display = new FoodDisplay(name, float.Parse(WeightText), newItem);
        }
        Foods.Add(display);

        // Clear Inputs
        SelectedItem = null;
        IngrediantText = string.Empty;
        WeightText = string.Empty;
    }

    // Finish creating a meal and returning to Meals page with the newly added entry.
    [RelayCommand]
    async Task AddMeal()
    {
        if (string.IsNullOrWhiteSpace(MealName))
            return;
        FoodDisplay newMeal = new FoodDisplay(this.MealName);
        MealItem f = new MealItem();

        // Create list of foods and their weights to combine into the meal.
        List<FoodItem> food_list = new List<FoodItem>();
        List<float> weight_list = new List<float>();
        foreach (FoodDisplay food in Foods) { 
            food_list.Add((FoodItem)food.Item);
            weight_list.Add(food.Value);
        }
        f.AddFoodsToMeal(food_list, weight_list);

        // Send meal to MealsViewModel and return to Meals Page.
        newMeal.AddFoodItem(f);
        await Shell.Current.GoToAsync("..",  new Dictionary<string, object> { ["Meal"] = newMeal });
    }
}
