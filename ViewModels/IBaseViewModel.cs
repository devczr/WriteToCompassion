using WriteToCompassion.Services.Settings;

namespace WriteToCompassion.ViewModels;

public interface IBaseViewModel
{
    public ISettingsService SettingsService { get; }

    public bool IsInitialized { get; set; }

    Task InitializeAsync();
}
