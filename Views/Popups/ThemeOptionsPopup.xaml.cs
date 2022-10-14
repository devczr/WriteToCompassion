using CommunityToolkit.Maui.Views;
using WriteToCompassion.Models.Popups;

namespace WriteToCompassion.Views.Popups;

public partial class ThemeOptionsPopup : Popup
{
	public ThemeOptionsPopup()
	{
		InitializeComponent();
    }

    void OKClicked(object? sender, EventArgs e) => Close();
    void CancelClicked(object? sender, EventArgs e) => Close();

    private void OnThemeChosen(object sender, EventArgs e) => Close();

}