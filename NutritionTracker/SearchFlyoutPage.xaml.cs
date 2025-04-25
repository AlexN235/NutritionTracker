namespace NutruitionTracker;

public partial class SearchFlyoutPage : FlyoutPage
{
	public SearchFlyoutPage()
    {
        InitializeComponent();
        foodFlyoutPage.GetSelectionCollection().SelectionChanged += OnSelectionChanged;
    }

    void OnSelectionChanged(object sender, SelectionChangedEventArgs e) 
	{
		var item = e.CurrentSelection.FirstOrDefault() as FoodDisplay;
		if (item != null)
		{
			if (!((IFlyoutPageController)this).ShouldShowSplitMode)
				IsPresented = false;
		}
		Detail = new NavigationPage(new FoodFacts(item.Name));
	}
}