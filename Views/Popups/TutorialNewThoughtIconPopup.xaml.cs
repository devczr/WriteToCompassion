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
        var scaleUpAnimation = new Animation(v => iconBorder.Scale = v, 1, 0.8, Easing.Linear);
        var scaleDownAnimation = new Animation(v => iconBorder.Scale = v, 0.8, 1, Easing.Linear);
        var scaleUp2Animation = new Animation(v => iconBorder.Scale = v, 1, 0.8, Easing.Linear);
        var scaleDown2Animation = new Animation(v => iconBorder.Scale = v, 0.8, 1, Easing.Linear);
        var rotateAnimation = new Animation(v => iconBorder.Rotation = v, 0, 180);


        parentAnimation.Add(0, 0.25, scaleUpAnimation);
        parentAnimation.Add(0.25, 0.5, scaleDownAnimation);
        parentAnimation.Add(0.5, 0.75, scaleUp2Animation);
        parentAnimation.Add(0.75, 1, scaleDown2Animation);
        parentAnimation.Add(0.95, 1, rotateAnimation);

        parentAnimation.Commit(iconBorder, "ChildAnimations", 16, 4000, null, null, () => true);
    }


}