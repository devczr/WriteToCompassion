using WriteToCompassion.Services.Navigation;
using WriteToCompassion.Services.Settings;

namespace WriteToCompassion.ViewModels;

public interface IBaseViewModel
{
    public INavigationService NavigationService { get; }
    public ISettingsService SettingsService { get; }

    public bool IsInitialized { get; set; }

    Task InitializeAsync();
}
