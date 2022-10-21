namespace WriteToCompassion.Services.Settings;

public interface ISettingsService
{
    bool DisplayTutorial { get; set; }

    bool UnreadOnly { get; set; }

    bool InstantText { get; set; }

    string ThemeChoice { get; set; }

    double CloudScale { get; set; }

    int MaxClouds { get; set; }
}

