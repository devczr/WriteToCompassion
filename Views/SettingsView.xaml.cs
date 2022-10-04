using WriteToCompassion.ViewModels;

namespace WriteToCompassion.Views;

public partial class SettingsView : ContentPage
{
	SettingsViewModel settingsViewModel;
	public SettingsView(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();
		BindingContext = settingsViewModel;
		this.settingsViewModel = settingsViewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

}