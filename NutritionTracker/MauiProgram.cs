using Microsoft.Extensions.Logging;
using NutruitionTracker.ViewModel;
using NutruitionTracker.NutritionFacts;

namespace NutruitionTracker;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddSingleton<NutritionDatabase>();

		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();

		builder.Services.AddSingleton<MyMeals>();
		builder.Services.AddSingleton<MyMealsViewModel>();

		builder.Services.AddTransient<InputMealPage>();
		builder.Services.AddTransient<InputMealViewModel>();

        builder.Services.AddTransient<FoodSearch>();
		builder.Services.AddTransient<FoodSearchViewModel>();

		builder.Services.AddTransient<FoodFacts>();
		builder.Services.AddTransient<FoodFactsViewModel>();

        builder.Services.AddTransient<DisplayDetailPage>();
        builder.Services.AddTransient<DisplayDetailViewModel>();



#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
