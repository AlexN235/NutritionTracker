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

    public string Name { get; set; }
    public float Value { get; set; }
    public Type TargeType { get; set; }
}
