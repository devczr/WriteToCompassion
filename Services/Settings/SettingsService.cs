namespace WriteToCompassion.Services.Settings;

public class SettingsService : ISettingsService
{
    //Intro tutorial to be displayed just on initial app start
    private const string IdDisplayTutorial = "display_tutorial";
    private readonly bool DisplayTutorialDefault = true;

    // Only display unread thoughts
    private const string IdUnreadOnly = "unread_only";
    private readonly bool UnreadOnlyDefault = false;


    // Light / Dark Themes (HomeView designed primarily for Dark theme)
    private const string IdThemeChoice = "theme_choice";
    private readonly string ThemeChoiceDefault = "Dark";

    // Size of Clouds on HomeView
    private const string IdCloudScale = "cloud_scale";
    private readonly double CloudScaleDefault = 1.1;

    // Max Clouds on HomeView
    private const string IdMaxClouds = "max_clouds";
    private readonly int MaxCloudsDefault = 5;

    // Instantly show cloud text or print by characters
    private const string IdInstantText = "instant_text";
    private readonly bool InstantTextDefault = false;


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
            switch (value)
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

    public double CloudScale
    {
        get => Preferences.Get(IdCloudScale, CloudScaleDefault);
        set => Preferences.Set(IdCloudScale, value);
    }

    public int MaxClouds
    {
        get => Preferences.Get(IdMaxClouds, MaxCloudsDefault);
        set => Preferences.Set(IdMaxClouds, value);
    }

    public bool InstantText
    {
        get => Preferences.Get(IdInstantText, InstantTextDefault);
        set => Preferences.Set(IdInstantText, value);
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
