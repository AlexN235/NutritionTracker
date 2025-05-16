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
    NutritionDatabase db;
    MyMealsViewModel myMealsVM;
    public InputMealViewModel(MyMealsViewModel myMealsVM)
    {
        foods = new ObservableCollection<FoodItem>();
        foodsWeight = new List<float>();
        db = new NutritionDatabase();
        this.myMealsVM = myMealsVM;
    }

    public void breakpause() { return; }

    [ObservableProperty]
    ObservableCollection<FoodItem> foods;
    [ObservableProperty]
    List<float> foodsWeight;

    [ObservableProperty]
    string ingrediantText;
    [ObservableProperty]
    string weightText;

    [ObservableProperty]
    string mealName;
    [ObservableProperty]
    string mealWeight;

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

    [RelayCommand]
    void Add() 
    {
        if (string.IsNullOrWhiteSpace(IngrediantText) || string.IsNullOrWhiteSpace(WeightText))
            return;
        FoodItem newItem = new FoodItem(db.GetClosestName(IngrediantText));
        foods.Add(newItem);
        foodsWeight.Add(float.Parse(WeightText));

        IngrediantText = string.Empty;
        WeightText = string.Empty;
    }

    [RelayCommand]
    async Task AddMeal()
    {
        if (string.IsNullOrWhiteSpace(MealName) || string.IsNullOrWhiteSpace(MealWeight))
            return;
        if (!Regex.Match(MealWeight, "^[0-9]+$").Success) 
        {
            return;
        }

        // Add FoodItem using name and add to FoodDisplay with name, weight, FoodItem
        FoodDisplay newMeal = new FoodDisplay(this.MealName, Int32.Parse(this.MealWeight));
        FoodItem f = new FoodItem();
        f.AddFoodsToFoodItem(foods.ToList(), foodsWeight);
        newMeal.AddFoodItem(f);
        myMealsVM.AddMeal(newMeal);
        await Shell.Current.GoToAsync("..");
    }

    public List<string> GetFromFood(string s) 
    {
        return db.GetFoodName(s);
    }
}
