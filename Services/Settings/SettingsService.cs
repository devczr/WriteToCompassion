namespace WriteToCompassion.Services.Settings;

public class SettingsService : ISettingsService
{
    //Intro tutorial to be displayed just on initial app start
    private const string IdDisplayTutorial = "display_tutorial";
    private readonly bool DisplayTutorialDefault = true;

    //Intro tutorial to be displayed just on initial app start
    private const string IdUnreadOnly = "unread_only";
    private readonly bool UnreadOnlyDefault = true;


    //Intro tutorial to be displayed just on initial app start
    private const string IdThemeChoice = "theme_choice";
    private readonly string ThemeChoiceDefault = "default";




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

    private void SetAppTheme(string theme)
    {
        switch (theme)
        {
            case "light":
                Application.Current.UserAppTheme = AppTheme.Light;
                break;

            case "dark":
                Application.Current.UserAppTheme = AppTheme.Dark;
                break;
            
            default:
                Application.Current.UserAppTheme = AppTheme.Unspecified;
                break;

        };
    }
}
