using Microsoft.Maui.Layouts;
using System.Collections.Specialized;

namespace NutruitionTracker;

public class FoodDisplayGroup : List<FoodDisplay>, IEquatable<FoodDisplayGroup>
{
    public event NotifyCollectionChangedEventHandler CollectionChanged;
    public string Name { get; private set; }
    public List<FoodDisplay> List { get; private set; }

    public FoodDisplayGroup(string name, List<FoodDisplay> fd) : base(fd)
    {
        Name = name;
        List = fd;
    }
    public Boolean SameGroup(string name) 
    {
        return this.Name == name;
    }
    public void AddToList(FoodDisplay food)
    { 
        this.Add(food);
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, food));
    }
    public bool Equals(FoodDisplayGroup? other)
    {
        if (this.Name == other.Name && this.List.Count == other.List.Count)
            return true;
        return false;
    }
}
