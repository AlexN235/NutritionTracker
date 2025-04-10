using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NutruitionTracker.NutritionFacts;
using System.Runtime.InteropServices;

namespace NutruitionTracker.ViewModel;

public partial class MainViewModel : ObservableObject
{
    NutritionDatabase db;

    public MainViewModel()
    {
        db  = new NutritionDatabase();
    }

    [ObservableProperty]
    public string text;

    [RelayCommand]
    void DoSomething()
    {
        Text = "it works";
    }

    [RelayCommand]
    async Task GoToMeals(string s) 
    {
        await Shell.Current.GoToAsync(nameof(MyMeals),
            new Dictionary<string, object>
            {
                ["NutritionDB"] = db
            });
    }

}
