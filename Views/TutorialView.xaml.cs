using WriteToCompassion.ViewModels;

namespace WriteToCompassion.Views;

public partial class TutorialView : ContentPage
{
	TutorialViewModel tutorialViewModel => BindingContext as TutorialViewModel;
	public TutorialView(TutorialViewModel tutorialViewModel)
	{
		InitializeComponent();
        BindingContext = tutorialViewModel;
    }


	protected override void OnNavigatedFrom(NavigatedFromEventArgs args)
	{
		base.OnNavigatedFrom(args);
		tutorialViewModel.FalsifyTutorialPreference();

	}


}