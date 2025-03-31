using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace NutruitionTracker.ViewModel;

public partial class InputMealViewModel : ObservableObject
{
    public InputMealViewModel()
    {
        Ingredients = new ObservableCollection<Ingredient>();
    }

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
}
