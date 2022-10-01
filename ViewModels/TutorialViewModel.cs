using WriteToCompassion.Services.Navigation;
using WriteToCompassion.Services.Settings;
using WriteToCompassion.Views;

namespace WriteToCompassion.ViewModels;

public partial class TutorialViewModel : BaseViewModel
{
    private readonly ISettingsService _settingsService;

	public TutorialViewModel(ISettingsService settingsService, INavigationService navigationService) : base(navigationService, settingsService)
	{
		_settingsService = settingsService;
	}

    public override Task InitializeAsync()
    {
        return Task.CompletedTask;
    }

	public void FalsifyTutorialPreference()
	{
		_settingsService.DisplayTutorial = false;
	}


    [RelayCommand]
	private async Task SkipTutorial()
	{
		await Shell.Current.GoToAsync(nameof(ThoughtsPage));
	}

}
