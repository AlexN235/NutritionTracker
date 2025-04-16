using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

using NutruitionTracker.NutritionFacts;
using SQLite;
using NutruitionTracker.NutritionFacts.Models;
namespace NutruitionTracker.ViewModel;

public partial class InputMealViewModel : ObservableObject
{
    NutritionDatabase db;
    MyMealsViewModel myMealsVM;
    public InputMealViewModel(MyMealsViewModel myMealsVM)
    {
        Ingredients = new ObservableCollection<Ingredient>();
        db = new NutritionDatabase();
        this.myMealsVM = myMealsVM;
    }

    public void breakpause() { return; }

    [ObservableProperty]
    ObservableCollection<Ingredient> ingredients;

    [ObservableProperty]
    string ingrediantText;

    [ObservableProperty]
    string weightText;

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

        Ingredient newItem = new Ingredient(db.GetClosestName(IngrediantText), WeightText, db.GetClosestID(IngrediantText));
        Ingredients.Add(newItem);

        IngrediantText = string.Empty;
        WeightText = string.Empty;
    }

    [RelayCommand]
    async Task AddMeal()
    {
        Meal newMeal = new Meal("testName", 10, this.ingredients.ToList(), db);
        myMealsVM.AddMeal(newMeal);
        await Shell.Current.GoToAsync("..");
    }

    public List<string> GetFromFood(string s) 
    {
        return db.GetFood(s);
    }
}
