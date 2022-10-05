using CommunityToolkit.Maui.Views;
using WriteToCompassion.Models.Popups;

namespace WriteToCompassion.Views.Popups;

public partial class AddThoughtPopupView : Popup
{
	public AddThoughtPopupView()
	{
		InitializeComponent();
	}

	private void Button_Clicked(object sender, EventArgs e)
	{
		Close();
	}
}