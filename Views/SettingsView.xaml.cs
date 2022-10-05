using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Views;
using WriteToCompassion.Models;
using WriteToCompassion.Models.Popups;
using WriteToCompassion.ViewModels;
using WriteToCompassion.Views.Popups;

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

    async void HandleEditClicked(object sender, EventArgs e)
    {
        var editThoughtPopup = new EditThoughtPopup();

        var result = await this.ShowPopupAsync(editThoughtPopup);
        if (result is bool boolResult)
        {
            if (boolResult)
            {
                Shell.Current.DisplaySnackbar("codebehind yes");
            }
            else
            {
                Shell.Current.DisplaySnackbar("codebehind no");
            }
        }

    }

    /*    public async Task DisplayPopup()
        {
            var editThoughtPopup = new EditThoughtPopup();
            var result = await this.ShowPopupAsync(editThoughtPopup);
        }*/

}