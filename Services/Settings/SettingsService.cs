namespace WriteToCompassion.Services.Settings;

public class SettingsService : ISettingsService
{
    //Intro tutorial to be displayed just on initial app start
    private const string IdDisplayTutorial = "display_tutorial";
    private readonly bool DisplayTutorialDefault = true;

    // Only display unread thoughts
    private const string IdUnreadOnly = "unread_only";
    private readonly bool UnreadOnlyDefault = true;


    // Light / Dark Themes (HomeView designed primarily for Dark theme)
    private const string IdThemeChoice = "theme_choice";
    private readonly string ThemeChoiceDefault = "Dark";

    // Size of Clouds on HomeView
    private const string IdCloudScale = "cloud_scale";
    private readonly double CloudScaleDefault = 1.1;


    public bool DisplayTutorial
    {
        get => Preferences.Get(IdDisplayTutorial, DisplayTutorialDefault);
        set => Preferences.Set(IdDisplayTutorial, value);
    }

    public bool UnreadOnly
    {
        get => Preferences.Get(IdUnreadOnly, UnreadOnlyDefault);
        set => Preferences.Set(IdUnreadOnly, value);
    }

    public string ThemeChoice
    {
        get => Preferences.Get(IdThemeChoice, ThemeChoiceDefault);
        set 
        {
            Preferences.Set(IdThemeChoice, value);
            SetAppTheme(ThemeChoice);
        } 
    }

    public double CloudScale
    {
        get => Preferences.Get(IdCloudScale, CloudScaleDefault);
        set => Preferences.Set(IdCloudScale, value);
    }

    private void SetAppTheme(string theme)
    {
        switch (theme)
        {
            case "Light":
                Application.Current.UserAppTheme = AppTheme.Light;
                break;

            case "Dark":
                Application.Current.UserAppTheme = AppTheme.Dark;
                break;

            case "System Default":
                Application.Current.UserAppTheme = AppTheme.Unspecified;
                break;

            default:
                Application.Current.UserAppTheme = AppTheme.Unspecified;
                break;

        };
    }
}
