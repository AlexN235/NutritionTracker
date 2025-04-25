using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NutruitionTracker.ViewModel;

public partial class FoodFactsViewModel : ObservableObject
{
    [ObservableProperty]
    string name;
    public FoodFactsViewModel() { }
    
}
