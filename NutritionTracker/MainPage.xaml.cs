using CommunityToolkit.Mvvm.ComponentModel;
using NutruitionTracker.ViewModel;

namespace NutruitionTracker
{
    public partial class MainPage : ContentPage
    {
        public MainPage() 
        {
            InitializeComponent();
        }
        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

    }

}
