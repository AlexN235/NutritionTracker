using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NutruitionTracker.NutritionFacts;
using System.Xml.Linq;

namespace NutruitionTracker.ViewModel;

public partial class DisplayDetailViewModel : FoodFactsViewModel, IQueryAttributable
{
    private FoodDisplay Item;

    public DisplayDetailViewModel(NutritionDatabase db) : base(db) { }

    [RelayCommand]
    async Task Delete()
    {
        await Shell.Current.GoToAsync("..", new Dictionary<string, object>
        {
            ["toDelete"] = this.Item
        });
    }
    public new void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.Count() == 0)
            return;
        FoodDisplay f = (FoodDisplay)query["Food"];
        SetInfo(f);
    }
    public void SetInfo(FoodDisplay food)
    {
        if (food == null)
            return;

        this.Item = food;
        this.Name = food.Name;

        List<string> names = food.Item.ItemsNames;
        List<float> values = food.Item.ItemsValue.ToList();

        FoodList = new List<FoodDisplay>();
        for (int i = 0; i < names.Count; i++)
            FoodList.Add(new FoodDisplay(names[i], values[i]));
    }
}
