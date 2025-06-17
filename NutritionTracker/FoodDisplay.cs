using System.Diagnostics;

namespace NutruitionTracker;

public class FoodDisplay
{
    public FoodDisplay(string name)
    {
        Name = name;
    }
    public FoodDisplay(string name, float value) : this(name)
    {
        Value = value;
    }
    public FoodDisplay(string name, float value, EdibleItem item) : this(name)
    {
        Value = value;
        Item = item;
    }

    public string Name { get; set; }
    public float Value { get; set; }
    public EdibleItem Item { get; set; }

    public void AddFoodItem(EdibleItem item) 
    {
        this.Item = item;
    }
    public DateTime GetDate() 
    {
        if (Item is MealItem Meal) 
            return Meal.Date;
        return DateTime.Now;
    }
    public override bool Equals(Object other) 
    {
        if (other is not FoodDisplay display) return false;

        if (this.Item is null)
            return this.Name == display.Name;
        else { 
            if (display.Item is null)
                return false;
            return this.Name == display.Name && this.Item.Equals(display.Item);
        }
    }
    public override int GetHashCode()
    {
        return this.Name.GetHashCode();
    }
}
