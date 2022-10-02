using CommunityToolkit.Maui.Views;
using WriteToCompassion.Models.Popups;

namespace WriteToCompassion.Views.Popups;

public partial class AddThoughtPopupView : Popup
{
	public AddThoughtPopupView(PopupSizeConstants popupSizeConstants)
	{
		InitializeComponent();

		Size = popupSizeConstants.Small;
	}

	private void Button_Clicked(object sender, EventArgs e)
	{
		Close();
	}
}