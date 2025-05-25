using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Xml.Linq;

namespace NutruitionTracker.ViewModel;

public partial class DisplayDetailViewModel : ObservableObject
{
    [ObservableProperty]
    public List<FoodDisplay> food;

    private FoodDisplay Item;

    public DisplayDetailViewModel()  { }

    public void Set(FoodDisplay food) 
    {
        Item = food;
        EdibleItem f = food.Item;
        List<string> names = f.itemsNames;
        List<float> values = f.itemsValue.ToList();

        List<FoodDisplay> temp = new List<FoodDisplay>();
        for (int i = 0; i < names.Count; i++)
            temp.Add(new FoodDisplay(names[i], values[i]));

        Food = temp;
        OnPropertyChanged();
    }

    [RelayCommand]
    async void Back() 
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    async void Delete()
    {
        await Shell.Current.GoToAsync("..", new Dictionary<string, object>
        {
            ["toDelete"] = this.Item
        });
    }

}
