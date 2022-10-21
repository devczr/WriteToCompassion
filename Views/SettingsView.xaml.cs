using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using WriteToCompassion.Models;
using WriteToCompassion.Models.Popups;
using WriteToCompassion.ViewModels;
using WriteToCompassion.Views.Popups;

namespace WriteToCompassion.Views;

public partial class SettingsView : ContentPage
{
    HomeViewModel homeViewModel;
	public SettingsView(SettingsViewModel settingsViewModel, HomeViewModel homeViewModel)
	{
		InitializeComponent();
		BindingContext = settingsViewModel;
        this.homeViewModel = homeViewModel;
	}

    async void HandleThemeTapped(object sender, EventArgs e)
    {
        var buttonPopup = new ThemeOptionsPopup();
        await this.ShowPopupAsync(buttonPopup);
    }

}