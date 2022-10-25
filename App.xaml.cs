namespace WriteToCompassion;

public partial class App : Application
{
    private readonly ISettingsService settingsService;

    public App(ISettingsService settingsService)
    {
        this.settingsService = settingsService;

        InitializeComponent();

        InitApp();

        MainPage = new AppShell();
    }

    private void InitApp()
    { 
        SessionService.GenSessionID();
        settingsService.ThemeChoice = settingsService.ThemeChoice;
    }
}


