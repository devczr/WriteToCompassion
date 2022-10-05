using WriteToCompassion.Models.Popups;
using WriteToCompassion.ViewModels;
using WriteToCompassion.Views.Popups;
using WriteToCompassion.Services;
using CommunityToolkit.Maui.Views;
using WriteToCompassion.Helpers;

namespace WriteToCompassion.Views;

public partial class ThoughtsPage : ContentPage
{
    readonly PopupSizeConstants popupSizeConstants;
    readonly ThoughtsViewModel vm;
    public ThoughtsPage(ThoughtsViewModel vm, PopupSizeConstants popupSizeConstants)
    {
        InitializeComponent();
        BindingContext = vm;
        this.vm = vm;
        this.popupSizeConstants = popupSizeConstants;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
     
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        ScreenHelper.UpdateScreenXYValues(width, height);
    }


    async void HandleCloudPopupButtonClicked(object sender, EventArgs e)
    {
        /*    var cloudPopup = new CloudPopup();
            await this.ShowPopupAsync(cloudPopup);*/

        var thoughtPopup = new AddThoughtPopupView();
        await this.ShowPopupAsync(thoughtPopup);
    }



}