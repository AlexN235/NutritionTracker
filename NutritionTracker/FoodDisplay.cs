namespace NutruitionTracker;

public class FoodDisplay : IEquatable<FoodDisplay>
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

    public bool Equals(FoodDisplay other) 
    {
        if(other == null || other.Item == null) return false;
        return this.Name == other.Name && this.Item.Equals(other.Item);
    }
}
