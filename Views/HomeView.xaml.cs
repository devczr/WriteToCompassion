namespace WriteToCompassion.Views;

public partial class HomeView : ContentPage
{
    private readonly HomeViewModel homeViewModel;
	public HomeView(HomeViewModel homeViewModel)
	{
		InitializeComponent();
		BindingContext = homeViewModel;
        this.homeViewModel = homeViewModel;
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        ScreenHelper.UpdateScreenXYValues(cloudGrid.Width, cloudGrid.Height, contentBorder.Width, contentBorder.Height, cloudlottie.Height);
     
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
/*
        if(homeViewModel.DisplayTutorialPopups)
        {
            await StartTutorialPopupChainAsync();
        }*/
    }

    async void HandleTutorial(object sender, EventArgs e)
    {
        /*        var tutorialPopup = new TutorialNewThoughtIconPopup();
                tutorialPopup.Anchor = newThoughtIcon;*/
/*
        tutorialPopup.HorizontalOptions = Microsoft.Maui.Primitives.LayoutAlignment.Start;
        tutorialPopup.VerticalOptions = Microsoft.Maui.Primitives.LayoutAlignment.Center;*/

    }

    async Task StartTutorialPopupChainAsync()
    {
        var mainTutPopup = new TutorialPopup();
        await this.ShowPopupAsync(mainTutPopup);
    }




}