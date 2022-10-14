namespace WriteToCompassion.Services.Settings;

public interface ISettingsService
{
    bool DisplayTutorial { get; set; }

    bool UnreadOnly { get; set; }

    string ThemeChoice { get; set; }

}

//TODO: Implement authentication
/*    string AuthAccessToken { get; set; }
    string AuthIdToken { get; set; }*/

