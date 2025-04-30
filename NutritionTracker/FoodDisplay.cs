namespace NutruitionTracker;

public class FoodDisplay
{
    public FoodDisplay(string name)
    {
        Name = name;
    }

    public FoodDisplay(string name, int value) : this(name)
    {
        Value = value;
    }

    public string Name { get; set; }
    public int Value { get; set; }
    public Type TargeType { get; set; }
}
