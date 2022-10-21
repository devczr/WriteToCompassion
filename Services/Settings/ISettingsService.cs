namespace WriteToCompassion.Services.Settings;

public interface ISettingsService
{
    bool DisplayTutorial { get; set; }

    bool UnreadOnly { get; set; }

    string ThemeChoice { get; set; }

    double CloudScale { get; set; }
}

