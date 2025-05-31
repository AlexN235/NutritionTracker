using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NutruitionTracker.NutritionFacts;
using System.Xml.Linq;

namespace NutruitionTracker.ViewModel;

public partial class DisplayDetailViewModel : FoodFactsViewModel, IQueryAttributable
{
    private FoodDisplay Item;

    public DisplayDetailViewModel(NutritionDatabase db) : base(db)  { }

    public void setName(FoodDisplay food) 
    {
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
        await Shell.Current.GoToAsync("..", new Dictionary<string, object>
        {
            ["toDelete"] = this.Item
        });
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.Count == 0 || query["Food"] == null)
            return;
        this.setName((FoodDisplay)query["Food"]);
    }

}
