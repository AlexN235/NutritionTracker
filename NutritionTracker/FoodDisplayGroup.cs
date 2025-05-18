using System.Collections.Specialized;

namespace NutruitionTracker;

public class FoodDisplayGroup : List<FoodDisplay>, INotifyCollectionChanged
{
    public event NotifyCollectionChangedEventHandler CollectionChanged;
    public string Name { get; private set; }
    public FoodDisplayGroup(string name, List<FoodDisplay> fd) : base(fd)
    {
        Name = name;
    }

    public Boolean Equals(string name) 
    {
        return this.Name == name;
    }

    public void AddToList(FoodDisplay food)
    { 
        this.Add(food);
        CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, food));
    } 
}
