namespace NutruitionTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Args: Route itself, type
            Routing.RegisterRoute(nameof(MyMeals), typeof(MyMeals));
            Routing.RegisterRoute(nameof(InputMealPage), typeof(InputMealPage));
            Routing.RegisterRoute(nameof(FoodSearch), typeof(FoodSearch));
            Routing.RegisterRoute(nameof(FoodFacts), typeof(FoodFacts));
            Routing.RegisterRoute(nameof(SearchFlyoutPage), typeof(SearchFlyoutPage));
        }

    }
}
