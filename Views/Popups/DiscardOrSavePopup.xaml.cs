using CommunityToolkit.Maui.Views;

namespace WriteToCompassion.Views.Popups;

public partial class DiscardOrSavePopup : Popup
{
	public DiscardOrSavePopup()
	{
		InitializeComponent();
	}

    void OnDiscardClicked(object? sender, EventArgs e) => Close(false);

    void OnSaveClicked(object? sender, EventArgs e) => Close(true);

}