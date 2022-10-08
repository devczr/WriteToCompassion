#if WINDOWS
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Windows.Graphics;
#endif
using WriteToCompassion.Services.Settings;


namespace WriteToCompassion;

public partial class App : Application
{
    private readonly ISettingsService _settingsService;


    public App(ISettingsService settingsService)
    {
        _settingsService = settingsService;
        InitializeComponent();
        MainPage = new AppShell();
    }
}


