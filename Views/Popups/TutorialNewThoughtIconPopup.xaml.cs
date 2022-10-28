using CommunityToolkit.Maui.Views;

namespace WriteToCompassion.Views.Popups;

public partial class TutorialNewThoughtIconPopup : Popup
{
	public TutorialNewThoughtIconPopup()
	{
		InitializeComponent();
        IdleAnimation();
	}


    private async void HandleNewThoughtIconTapped(object sender, TappedEventArgs e)
    {
        Close();
        await Shell.Current.GoToAsync(nameof(NewThoughtEditorView), true);
    }


    private async void IdleAnimation()
    {
        var parentAnimation = new Animation();
        var scaleDownAnimation = new Animation(v => iconBorder.Scale = v, 1, 0.9, Easing.Linear);
        var scaleUpAnimation = new Animation(v => iconBorder.Scale = v, 0.9, 1, Easing.Linear);
        var scaleDown2Animation = new Animation(v => iconBorder.Scale = v, 1, 0.9, Easing.Linear);
        var scaleUp2Animation = new Animation(v => iconBorder.Scale = v, 0.9, 1, Easing.Linear);
        var scaleDown3Animation = new Animation(v => iconBorder.Scale = v, 1, 0.9, Easing.Linear);
        var scaleUp3Animation = new Animation(v => iconBorder.Scale = v, 0.9, 1, Easing.Linear);
        var scaleDown4Animation = new Animation(v => iconBorder.Scale = v, 1, 0.9, Easing.Linear);
        var scaleUp4Animation = new Animation(v => iconBorder.Scale = v, 0.9, 1, Easing.Linear);
        var rotateAnimation = new Animation(v => iconBorder.Rotation = v, 0, 90);


        parentAnimation.Add(0, 0.12, scaleDownAnimation);
        parentAnimation.Add(0.12, 0.25, scaleUpAnimation);
        parentAnimation.Add(0.25, 0.37, scaleDown2Animation);
        parentAnimation.Add(0.37, 0.5, scaleUp2Animation);
        parentAnimation.Add(0.5, 0.62, scaleDown3Animation);
        parentAnimation.Add(0.62, 0.75, scaleUp3Animation);
        parentAnimation.Add(0.75, 0.87, scaleDown4Animation);
        parentAnimation.Add(0.87, 1, scaleUp4Animation);
        parentAnimation.Add(0.95, 1, rotateAnimation);

        parentAnimation.Commit(iconBorder, "ChildAnimations", 16, 5000, null, null, () => true);
    }


}