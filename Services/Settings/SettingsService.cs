namespace WriteToCompassion.Services.Settings;

public class SettingsService : ISettingsService
{
    //Intro tutorial to be displayed just on initial app start
    private const string IdDisplayTutorial = "display_tutorial";
    private readonly bool DisplayTutorialDefault = true;
    
/*    private const string AccessToken = "access_token";
    private const string IdToken = "id_token";*/
    

    public bool DisplayTutorial
    {
        get => Preferences.Get(IdDisplayTutorial, DisplayTutorialDefault);
        set => Preferences.Set(IdDisplayTutorial, value);
    }



}
