﻿using CommunityToolkit.Mvvm.ComponentModel;
namespace NutruitionTracker.ViewModel;

using CommunityToolkit.Mvvm.Input;
using NutruitionTracker;

public partial class FoodSearchViewModel : ObservableObject
{
    [ObservableProperty]
    string searchBar;
    [ObservableProperty]
    FoodDisplay selectedItem;
    [ObservableProperty]
    public List<FoodDisplay> searchResults;

    public FoodSearchViewModel() { }

    [RelayCommand]
    async Task GoToFoodFacts() 
    {
        if (SelectedItem == null)
            return;

        Dictionary<string, object> dict = new Dictionary<string, object>
        {
            ["Food"] = SelectedItem
        };
        await Shell.Current.GoToAsync(nameof(FoodFacts), dict);
    }

    [RelayCommand]
    async Task Back() 
    {
        await Shell.Current.GoToAsync("..");
    }
}
