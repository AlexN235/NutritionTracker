using NutruitionTracker.NutritionFacts;
using NutruitionTracker.ViewModel;

namespace NutruitionTracker;

public partial class MyMeals : ContentPage, IQueryAttributable
{
    private MyMealsViewModel VM;
    public MyMeals(MyMealsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        VM = vm;
    }

    public void AddMeal(FoodDisplay newMeal) 
    {
        VM.AddMeal(newMeal);
    }

    void OnSelectionChange(object sender, SelectionChangedEventArgs e)
    {
        //FoodDisplay previous = (e.PreviousSelection.FirstOrDefault() as FoodDisplay);
        FoodDisplay current = (e.CurrentSelection.FirstOrDefault() as FoodDisplay);
        //if (previous == null || current == null)
        //    return;

        if (current != null) //previous.Equals(current))
        {
            VM.GoToDetails(current);
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.Count == 0)
            return;
        FoodDisplay dis = query["toDelete"] as FoodDisplay;

        VM.Delete(dis);
    }

}