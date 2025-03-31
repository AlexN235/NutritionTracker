using NutruitionTracker.ViewModel;

namespace NutruitionTracker;

public partial class InputMealPage : ContentPage
{
	public InputMealPage(InputMealViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}