namespace WriteToCompassion.Views;

public partial class LibraryView : ContentPage
{
    double openY = 80;
    double lastPanY = 0;
    bool drawerOpen = false;
    public LibraryView(LibraryViewModel libraryViewModel)
	{
		InitializeComponent();
		BindingContext = libraryViewModel;
	}
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        // backdrop not responding to first toggle from Sort Button on Android
        // simple workaround is to toggle the property here until I figure out the cause
        backdrop.IsVisible = true;
        backdrop.IsVisible = false;
        backdrop.Opacity = 0;
        backdrop.Opacity = 0.4;
    }
    async void SortClicked(object sender, EventArgs e)
    {
        if (drawerOpen)
        {
            await CloseDrawer();
        }
        else
        {
            await OpenDrawer();
        }
    }
    async void BackdropTapped(object sender, TappedEventArgs e)
    {
        if (drawerOpen)
            await CloseDrawer();
    }

    async void DrawerPan(object sender, PanUpdatedEventArgs e)
    {
        if (e.StatusType == GestureStatus.Running)
        {
            lastPanY = e.TotalY;
            if (e.TotalY > 0)
            {
                bottomDrawer.TranslationY = openY + e.TotalY;
            }

        }
        else if (e.StatusType == GestureStatus.Completed)
        {
            if (lastPanY < 110)
                await OpenDrawer();
            else
                await CloseDrawer();
        }
    }


    async Task OpenDrawer()
    {
        backdrop.IsVisible = true;
        backdrop.InputTransparent = false;


        await bottomDrawer.TranslateTo(0, 80, 100, easing: Easing.SinIn);
        drawerOpen = true;

    }

    async Task CloseDrawer()
    {
        backdrop.IsVisible = false;
        backdrop.InputTransparent = true;

        await bottomDrawer.TranslateTo(0, 425, 100, easing: Easing.SinIn);
        drawerOpen = false;

    }




}