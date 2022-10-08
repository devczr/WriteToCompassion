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
        ScreenHelper.UpdateScreenXYValues(width, height);
    }



}