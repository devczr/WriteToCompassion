using WriteToCompassion.Services.Settings;
using WriteToCompassion.Views;

namespace WriteToCompassion.Services.Navigation;
public class MauiNavigationService : INavigationService
{
    private readonly ISettingsService _settingsService;

    public MauiNavigationService(ISettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    public async Task InitializeAsync()
    {
        if (_settingsService.DisplayTutorial)
            await Shell.Current.GoToAsync(nameof(TutorialView));
        else
            await Shell.Current.GoToAsync(nameof(ThoughtsPage));
    }


    public Task NavigateToAsync(string route, IDictionary<string, object> routeParameters = null)
    {
        var shellNavigation = new ShellNavigationState(route);

        return routeParameters != null
            ? Shell.Current.GoToAsync(shellNavigation, routeParameters)
            : Shell.Current.GoToAsync(shellNavigation);
    }

    public Task PopAsync() =>
        Shell.Current.GoToAsync("..");
}