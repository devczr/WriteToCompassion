using WriteToCompassion.Services.Settings;
using WriteToCompassion.Views;

namespace WriteToCompassion.ViewModels;

public partial class TutorialViewModel : BaseViewModel
{
    private readonly ISettingsService _settingsService;

	public TutorialViewModel(ISettingsService settingsService) : base(settingsService)
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
}
