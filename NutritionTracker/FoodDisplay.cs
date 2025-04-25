namespace NutruitionTracker;

public class FoodDisplay
{
    public FoodDisplay(string name)
    {
        Name = name;
    }

    public string Name { get; set; }
    public Type TargeType { get; set; }
}
