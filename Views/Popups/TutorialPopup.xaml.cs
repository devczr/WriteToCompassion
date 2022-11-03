using CommunityToolkit.Maui.Views;
using System;
using WriteToCompassion.ViewModels.Popups;

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
        /*        A lot of times we can be harsh on ourselves, or unnecessarily self-critical.But we can choose to add a little self-compassion or positivity into our thoughts as well.*/

        await Task.Delay(1000);
        await introLabel1.FadeTo(1);
        await Task.Delay(350);
        await introLabel2.FadeTo(1);
        await Task.Delay(350);
        await introLabel3.FadeTo(1);
        await Task.Delay(350);
        await introLabel4.FadeTo(1);
        await checkItOutButton.TranslateTo(0, 0, 500, Easing.CubicInOut);

    }

}