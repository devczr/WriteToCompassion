using CommunityToolkit.Maui.Views;


namespace WriteToCompassion.Views.Popups;

public partial class EditThoughtPopup : Popup
{
	public EditThoughtPopup()
	{
		InitializeComponent();
	}

    void OnYesButtonClicked(object? sender, EventArgs e) => Close(true);

    void OnCancelButtonClicked(object? sender, EventArgs e) => Close(false);

}