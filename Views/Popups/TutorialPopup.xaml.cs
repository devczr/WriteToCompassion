using CommunityToolkit.Maui.Views;
using System;
using WriteToCompassion.ViewModels.Popups;

namespace WriteToCompassion.Views.Popups;

public partial class TutorialPopup : Popup
{

    public TutorialPopup()
	{
		InitializeComponent();
        DisplayIntroText();
	}



	
	private async Task DisplayIntroText()
	{
        /*        A lot of times we can be harsh on ourselves, or unnecessarily self-critical.But we can choose to add a little self-compassion or positivity into our thoughts as well.*/

        await Task.Delay(1000);
        await introLabel1.FadeTo(1);
        await Task.Delay(3000);
        await introThoughtType.FadeTo(1);
        await Task.Delay(1500);
        await introThoughtType.FadeTo(0, 250);
        introThoughtType.Text = "compassionate thoughts";
        await introThoughtType.FadeTo(1, 250);
        await Task.Delay(750);
        await introThoughtType.FadeTo(0, 250);
        introThoughtType.Text = "positive thoughts";
        await introThoughtType.FadeTo(1, 250);
        await Task.Delay(750);
        await introThoughtType.FadeTo(0, 250);
        introThoughtType.Text = "encouraging thoughts";
        await introThoughtType.FadeTo(1, 250);
        await Task.Delay(750);
        await introThoughtType.FadeTo(0, 250);
        introThoughtType.Text = "peaceful thoughts";
        await introThoughtType.FadeTo(1, 250);
        await Task.Delay(750);
        await introThoughtType.FadeTo(0, 250);
        introThoughtType.Text = "humorous thoughts";
        await introThoughtType.FadeTo(1, 250);
        await Task.Delay(750);
        await introThoughtType.FadeTo(0, 250);
        introThoughtType.Text = "energizing thoughts";
        await introThoughtType.FadeTo(1, 250);
        await Task.Delay(750);
        await introThoughtType.FadeTo(0, 250);
        introThoughtType.Text = "your thoughts";
        await introThoughtType.FadeTo(1, 250);

    }

}