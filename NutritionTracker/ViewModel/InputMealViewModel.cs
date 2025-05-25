using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

using NutruitionTracker.NutritionFacts;
using SQLite;
using NutruitionTracker.NutritionFacts.Models;
using System.Text.RegularExpressions;
namespace NutruitionTracker.ViewModel;

public partial class InputMealViewModel : ObservableObject
{
    private NutritionDatabase db;
    private MyMealsViewModel myMealsVM;
    public InputMealViewModel(MyMealsViewModel myMealsVM)
    {
        foods = new ObservableCollection<FoodDisplay>();
        db = new NutritionDatabase();
        this.myMealsVM = myMealsVM;
    }

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

    partial void OnQueryChanged(string? oldValue, string newValue)
    {
        SearchResult = db.GetFoodName(newValue);
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
        string name = db.GetClosestName(IngrediantText);
        FoodItem newItem = new FoodItem(name);
        FoodDisplay display = new FoodDisplay(name, float.Parse(WeightText), newItem);
        foods.Add(display);

        IngrediantText = string.Empty;
        WeightText = string.Empty;
    }

    // Finish creating a meal and returning to Meals page with the newly added entry.
    [RelayCommand]
    async Task AddMeal()
    {
        if (string.IsNullOrWhiteSpace(MealName))
            return;
        
        // Create new meal to send back to Meal page.
        FoodDisplay newMeal = new FoodDisplay(this.MealName);
        MealItem f = new MealItem();

        // Create list of foods and their weights to combine into the meal.
        List<FoodItem> food_list = new List<FoodItem>();
        List<float> weight_list = new List<float>();
        foreach (FoodDisplay food in foods) { 
            food_list.Add((FoodItem)food.Item);
            weight_list.Add(food.Value);
        }
        f.AddFoodsToFoodItem(food_list, weight_list);

        // Send meal to MealsViewModel and return to Meals Page.
        newMeal.AddFoodItem(f);
        myMealsVM.AddMeal(newMeal);
        await Shell.Current.GoToAsync("..");
    }

    public List<string> GetFromFood(string s) 
    {
        return db.GetFoodName(s);
    }


}
