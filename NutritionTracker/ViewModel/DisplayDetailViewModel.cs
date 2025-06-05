using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NutruitionTracker.NutritionFacts;
using System.Xml.Linq;

namespace NutruitionTracker.ViewModel;

public partial class DisplayDetailViewModel : ObservableObject, IQueryAttributable
{
    [ObservableProperty]
    public string name;

    [ObservableProperty]
    public List<FoodDisplay> foodList;

    protected NutritionDatabase database;

    private FoodDisplay Item;

    public DisplayDetailViewModel(NutritionDatabase db)
    {
        this.database = db;
        foodList = new List<FoodDisplay>();
    }

    public void setName(FoodDisplay food) 
    {
        
        if (food == null)
            return;

        this.Item = food;
        this.Name = food.Name;

        EdibleItem f = food.Item;
        List<string> names = f.itemsNames;
        List<float> values = f.itemsValue.ToList();

        List<FoodDisplay> temp = new List<FoodDisplay>();
        for (int i = 0; i < names.Count; i++)
            temp.Add(new FoodDisplay(names[i], values[i]));

        FoodList = temp;
        OnPropertyChanged();
    }

    [RelayCommand]
    async void Delete()
    {
        Task.Delay(100);
        await Shell.Current.GoToAsync("..", new Dictionary<string, object>
        {
            ["toDelete"] = this.Item
        });
    }

    [RelayCommand]
    public async Task Back()
    {
        Task.Delay(100);
        await Shell.Current.GoToAsync("..");
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        FoodDisplay f = (FoodDisplay)query["Food"];
        setName(f);
    }

}
