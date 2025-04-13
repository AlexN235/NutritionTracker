using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NutruitionTracker.NutritionFacts;
using System.Runtime.InteropServices;

namespace NutruitionTracker.ViewModel;

public partial class MainViewModel : ObservableObject
{

    public MainViewModel()
    {
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
        await Shell.Current.GoToAsync(nameof(MyMeals));
    }

}
