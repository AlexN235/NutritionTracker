using Microsoft.Extensions.Logging;
using NutruitionTracker.ViewModel;

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

		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<MainViewModel>();

		builder.Services.AddSingleton<MyMeals>();
		builder.Services.AddSingleton<MyMealsViewModel>();

		builder.Services.AddTransient<InputMealPage>();
		builder.Services.AddTransient<InputMealViewModel>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
