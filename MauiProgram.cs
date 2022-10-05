using CommunityToolkit.Maui;
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
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
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
        mauiAppBuilder.Services.AddSingleton<MainViewModel>();
        mauiAppBuilder.Services.AddSingleton<TutorialViewModel>();
        mauiAppBuilder.Services.AddTransient<ThoughtsViewModel>();
        mauiAppBuilder.Services.AddTransient<EditorViewModel>();
        mauiAppBuilder.Services.AddTransient<SettingsViewModel>();
        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterViews(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<TutorialView>();
        mauiAppBuilder.Services.AddTransient<SettingsView>();
        mauiAppBuilder.Services.AddTransient<ThoughtsPage>();
        mauiAppBuilder.Services.AddTransient<EditorView>();

        return mauiAppBuilder;
    }

    public static MauiAppBuilder RegisterPopups(this MauiAppBuilder mauiAppBuilder)
    {
        mauiAppBuilder.Services.AddSingleton<PopupSizeConstants>();
        mauiAppBuilder.Services.AddTransient<AddThoughtPopupView>();
        mauiAppBuilder.Services.AddTransient<EditThoughtPopup>();
        return mauiAppBuilder;
    }

    static void RegisterEssentials(in IServiceCollection services)
    {
        services.AddSingleton<IDeviceInfo>(DeviceInfo.Current);
        services.AddSingleton<IDeviceDisplay>(DeviceDisplay.Current);
    }


}
