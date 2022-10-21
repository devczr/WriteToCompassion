using CommunityToolkit.Maui.Behaviors;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Text;
namespace WriteToCompassion.Views;

public partial class HomeView : ContentPage
{
	public HomeView(HomeViewModel homeViewModel)
	{
		InitializeComponent();
		BindingContext = homeViewModel;
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        ScreenHelper.UpdateScreenXYValues(cloudGrid.Width, cloudGrid.Height, contentBorder.Width, contentBorder.Height, cloudlottie.Height);
     
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    private void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {

    }
}