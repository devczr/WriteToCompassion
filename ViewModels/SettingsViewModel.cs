using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WriteToCompassion.Services;
using WriteToCompassion.Services.Navigation;
using WriteToCompassion.Services.Settings;
using WriteToCompassion.Views;

namespace WriteToCompassion.ViewModels;

public partial class SettingsViewModel : BaseViewModel
{

    private readonly ISettingsService _settingsService;

    [ObservableProperty]
    string tutorialStatus;

    public SettingsViewModel(ISettingsService settingsService, INavigationService navigationService) : base(navigationService, settingsService)
    {
        _settingsService = settingsService;
        tutorialStatus = _settingsService.DisplayTutorial.ToString();
    }

    [RelayCommand]
    private void ToggleDisplayTutorial()
    {
        if (_settingsService.DisplayTutorial)
        {
            _settingsService.DisplayTutorial = false;
            TutorialStatus = "False";
            Shell.Current.DisplayAlert("Tutorial Disabled", "Preference saved. Tutorial will not show on app startup.", "OK");
        }

        else
        {
            _settingsService.DisplayTutorial = true;
            TutorialStatus = "True";
            Shell.Current.DisplayAlert("Tutorial Enabled", "Preference saved. Tutorial will show on next app startup.", "OK");
        }
    }

    [RelayCommand]
    async Task GoToThoughtsAsync()
    {
        await Shell.Current.GoToAsync(nameof(ThoughtsPage));
    }

    [RelayCommand]
    async Task GoToMainPageAsync()
    {
        await Shell.Current.GoToAsync(nameof(MainPage));
    }


}
