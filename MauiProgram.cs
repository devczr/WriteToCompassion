using CommunityToolkit.Maui;
using MauiGestures;
using SkiaSharp.Views.Maui.Controls.Hosting;
using WriteToCompassion.Models.Popups;
using WriteToCompassion.Services;
using WriteToCompassion.Services.Settings;
using WriteToCompassion.Services.Thoughts;
using WriteToCompassion.ViewModels;
using WriteToCompassion.Views;
using WriteToCompassion.Views.Popups;
namespace WriteToCompassion;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseSkiaSharp()
            .UseMauiCommunityToolkit()
            .AddAdvancedGestures()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("JosefinSans-Regular.ttf", "Josefin");
                fonts.AddFont("JosefinSans-ExtraLight.ttf", "JosefinThin");
            })
            .RegisterAppServices()
            .RegisterViewModels()
            .RegisterViews()
            .RegisterPopups();
        RegisterEssentials(builder.Services);
        return builder.Build();
    }

    public static MauiAppBuilder RegisterAppServices(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<ISettingsService, SettingsService>();
        mauiAppBuilder.Services.AddSingleton<NoteService>();
        mauiAppBuilder.Services.AddSingleton<ThoughtsService>();
        mauiAppBuilder.Services.AddSingleton<AnimationService>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViewModels(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<TutorialViewModel>();
        mauiAppBuilder.Services.AddTransient<HomeViewModel>();
        mauiAppBuilder.Services.AddTransient<EditorViewModel>();
        mauiAppBuilder.Services.AddTransient<NewThoughtEditorViewModel>();
        mauiAppBuilder.Services.AddTransient<SettingsViewModel>();
        mauiAppBuilder.Services.AddTransient<LibraryViewModel>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<TutorialView>();
        mauiAppBuilder.Services.AddTransient<SettingsView>();
        mauiAppBuilder.Services.AddTransient<HomeView>();
        mauiAppBuilder.Services.AddTransient<EditorView>();
        mauiAppBuilder.Services.AddTransient<NewThoughtEditorView>();
        mauiAppBuilder.Services.AddTransient<LibraryView>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterPopups(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<PopupSizeConstants>();
        return mauiAppBuilder;
    }

    static void RegisterEssentials(in IServiceCollection services)
    {
        services.AddSingleton<IDeviceInfo>(DeviceInfo.Current);
        services.AddSingleton<IDeviceDisplay>(DeviceDisplay.Current);
    }


}
