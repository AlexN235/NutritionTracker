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
        if (Item.GetType() == typeof(MealItem))
            return ((MealItem)Item).Date;
        return DateTime.Now;
    }

    public override bool Equals(Object other) 
    {
        Debug.WriteLine("Inside Equals");
        if (other is not FoodDisplay) return false;
        if(other == null) return false;

        if (this.Item == null)
            return this.Name == ((FoodDisplay)other).Name;// && this.Item.Equals(((FoodDisplay)other).Item);
        else { 
            if (((FoodDisplay)other).Item == null)
                return false;
            return this.Name == ((FoodDisplay)other).Name && this.Item.Equals(((FoodDisplay)other).Item);
        }
    }

    public override int GetHashCode()
    {
        return this.Name.GetHashCode();
    }
}
