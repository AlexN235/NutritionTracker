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

    public FoodDisplay(string name, float value, FoodItem item) : this(name)
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
}
