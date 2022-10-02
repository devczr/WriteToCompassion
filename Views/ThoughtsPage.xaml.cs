using WriteToCompassion.Models.Popups;
using WriteToCompassion.ViewModels;
using WriteToCompassion.Views.Popups;
using WriteToCompassion.Services;
using CommunityToolkit.Maui.Views;

namespace WriteToCompassion.Views;

public partial class ThoughtsPage : ContentPage
{
    readonly PopupSizeConstants popupSizeConstants;
    readonly ThoughtsViewModel vm;
    readonly AnimationService animationService;
    public ThoughtsPage(ThoughtsViewModel vm, PopupSizeConstants popupSizeConstants, AnimationService animationService)
    {
        InitializeComponent();
        BindingContext = vm;
        this.vm = vm;
        this.popupSizeConstants = popupSizeConstants;
        this.animationService = animationService;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
     
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        animationService.UpdateScreenXYValues(width, height);
    }


    async void HandleCloudPopupButtonClicked(object sender, EventArgs e)
    {
        /*    var cloudPopup = new CloudPopup();
            await this.ShowPopupAsync(cloudPopup);*/

        var thoughtPopup = new AddThoughtPopupView(popupSizeConstants);
        await this.ShowPopupAsync(thoughtPopup);
    }

    private void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
    {

    }
}