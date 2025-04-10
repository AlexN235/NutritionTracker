using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

using NutruitionTracker.NutritionFacts;
using SQLite;
using NutruitionTracker.NutritionFacts.Models;
namespace NutruitionTracker.ViewModel;

[QueryProperty(nameof(NutritionDatabase), nameof(NutritionDatabase))]
public partial class InputMealViewModel : ObservableObject
{

    NutritionDatabase db;
    public InputMealViewModel()
    {
        Ingredients = new ObservableCollection<Ingredient>();
        breakpause();
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

        Ingredient newItem = new Ingredient(IngrediantText, WeightText);
        Ingredients.Add(newItem);

        IngrediantText = string.Empty;
        WeightText = string.Empty;
    }
    

    void AddMeal()
    {

    }



    public List<string> GetFromFood(string s) 
    {
        return db.GetFood(s);
    }
}
