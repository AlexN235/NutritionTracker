using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace NutruitionTracker.ViewModel;

public partial class MainViewModel : ObservableObject
{


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
        await Shell.Current.GoToAsync(nameof(Meals));
    }
}
