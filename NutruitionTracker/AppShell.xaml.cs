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
        }

    }
}
