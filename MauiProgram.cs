using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Storage;
using Microsoft.Extensions.Logging;
using WildlifeTrackerSystem.ViewModels;
using WildlifeTrackerSystem.Views;

namespace WildlifeTrackerSystem
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkitCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<AnimalManager>();
            builder.Services.AddSingleton<FoodManager>();
            builder.Services.AddSingleton<FileManager>();
            builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddTransient<CreatePage>();
            builder.Services.AddTransient<ListPage>();
            builder.Services.AddTransient<EditPage>();
            builder.Services.AddTransient<FoodPage>();

            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddTransient<CreateViewModel>();
            builder.Services.AddTransient<ListViewModel>();
            builder.Services.AddTransient<EditViewModel>();
            builder.Services.AddTransient<FoodViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
