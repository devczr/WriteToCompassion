using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using WriteToCompassion.Models;
using WriteToCompassion.Models.Popups;
using WriteToCompassion.ViewModels;
using WriteToCompassion.Views.Popups;

namespace WriteToCompassion.Views;

public partial class SettingsView : ContentPage
{
	public SettingsView(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();
		BindingContext = settingsViewModel;
	}

    async void HandleButtonPopupButtonClicked(object sender, EventArgs e)
    {
        var buttonPopup = new ThemeOptionsPopup();
        await this.ShowPopupAsync(buttonPopup);
    }

}