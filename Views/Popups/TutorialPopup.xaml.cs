namespace WriteToCompassion.Views.Popups;

public partial class TutorialPopup : Popup
{

    public TutorialPopup()
	{
		InitializeComponent();
        checkItOutButton.TranslateTo(0, 500,0);
        DisplayIntroText();
	}

    void OnCheckItOutButtonClicked(object? sender, EventArgs e) => Close(false);



    private async Task DisplayIntroText()
	{
        await Task.Delay(1000);
        await introLabel1.FadeTo(1);
        await Task.Delay(500);
        await introLabel2.FadeTo(1);
        await Task.Delay(500);
        await introLabel3.FadeTo(1);
        await Task.Delay(500);
        await introLabel4.FadeTo(1);
        await checkItOutButton.TranslateTo(0, 0, 750, Easing.BounceIn);
    }

}