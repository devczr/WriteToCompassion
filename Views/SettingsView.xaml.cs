namespace WriteToCompassion.Views;

public partial class SettingsView : ContentPage
{
	public SettingsView(SettingsViewModel settingsViewModel)
	{
		InitializeComponent();
		BindingContext = settingsViewModel;
	}

    async void HandleThemeTapped(object sender, EventArgs e)
    {
        var buttonPopup = new ThemeOptionsPopup();
        await this.ShowPopupAsync(buttonPopup);
    }

}