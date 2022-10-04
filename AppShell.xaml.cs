using WriteToCompassion.Services.Navigation;
using WriteToCompassion.Views;
using WriteToCompassion.Views.Popups;

namespace WriteToCompassion;

public partial class AppShell : Shell
{
	private readonly INavigationService _navigationService;
	public AppShell(INavigationService navigationService)
	{
		_navigationService = navigationService;

		AppShell.InitializeRouting();
		InitializeComponent();
	}


	protected override async void OnHandlerChanged()
	{
		base.OnHandlerChanged();

		if (Handler is not null)
		{
			await _navigationService.InitializeAsync();
		}
	}

	// TODO: delete comment if relative routing refresh works
	private static void InitializeRouting()
	{
        Routing.RegisterRoute(nameof(ThoughtsPage), typeof(ThoughtsPage));
        Routing.RegisterRoute(nameof(TutorialView), typeof(TutorialView));
        Routing.RegisterRoute(nameof(SettingsView), typeof(SettingsView));
        Routing.RegisterRoute(nameof(AddThoughtPopupView), typeof(AddThoughtPopupView));
    }
}
