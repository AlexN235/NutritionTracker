namespace NutruitionTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            //Args: Route itself, type
            Routing.RegisterRoute(nameof(Meals), typeof(Meals));
            Routing.RegisterRoute(nameof(InputMealPage), typeof(InputMealPage));
        }


    }
}
