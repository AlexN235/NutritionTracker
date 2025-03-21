using NutruitionTracker.ViewModel;

namespace NutruitionTracker;

public partial class Meals : ContentPage
{
	public Meals(MealsViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}