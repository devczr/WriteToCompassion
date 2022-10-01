using WriteToCompassion.ViewModels;

namespace WriteToCompassion.Views;

public partial class SettingsView : ContentPage
{
	public SettingsView(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();
		BindingContext = settingsViewModel;
	}
}