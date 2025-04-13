using CommunityToolkit.Mvvm.ComponentModel;
using NutruitionTracker.ViewModel;

namespace NutruitionTracker
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage(MainViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }

    }

}
