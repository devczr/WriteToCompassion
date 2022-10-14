using CommunityToolkit.Maui.Views;

namespace WriteToCompassion.Views.Popups;

public partial class CloudPopup : Popup
{
    public CloudPopup() => InitializeComponent();

    void OnOKButtonClicked(object? sender, EventArgs e) => Close();
}