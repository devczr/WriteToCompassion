using CommunityToolkit.Maui.Views;
using WriteToCompassion.Models.Popups;

namespace WriteToCompassion.Views.Popups;

public partial class ThemeOptionsPopup : Popup
{
	public ThemeOptionsPopup()
	{
		InitializeComponent();
    }
	void OnClickClosePopup(object? sender, EventArgs e) => Close();

}