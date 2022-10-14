namespace WriteToCompassion.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{
    private readonly ISettingsService settingsService;

    private string _themeChoice;

    public string ThemeChoice
    {
        get => _themeChoice;
        set
        {
            SetProperty(ref _themeChoice, value);
            ChangeTheme();
        }
    }

    public SettingsViewModel(ISettingsService settingsService) : base(settingsService)
    {
        this.settingsService = settingsService;
        _themeChoice = settingsService.ThemeChoice;
        Title = "Settings";
    }



    private void ChangeTheme()
    {
        settingsService.ThemeChoice = _themeChoice;
    }



    [RelayCommand]
    private void ToggleDisplayTutorial()
    {
        if (settingsService.DisplayTutorial)
        {
            settingsService.DisplayTutorial = false;
            Shell.Current.DisplayAlert("Tutorial Disabled", "Preference saved. Tutorial will not show on app startup.", "OK");
        }

        else
        {
            settingsService.DisplayTutorial = true;
            Shell.Current.DisplayAlert("Tutorial Enabled", "Preference saved. Tutorial will show on next app startup.", "OK");
        }
    }

}
