using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;
using SkiaSharp;
using System.Timers;
using WriteToCompassion.Models;
using WriteToCompassion.Models.Popups;
using WriteToCompassion.ViewModels;
using WriteToCompassion.Views.Popups;
using SkiaSharp.Extended;
using SkiaSharp.Views.Maui;
using CommunityToolkit.Maui.Alerts;
using WriteToCompassion.Animations;
using WriteToCompassion.Services;

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

/*    public void DisplayPopup()
    {
        var popup = new AddThoughtPopupView(popupSizeConstants);
        this.ShowPopup(popup);
    }

    private void AddThoughtPopupButton(object sender, EventArgs e)
    {
        DisplayPopup();
        cloudlottie.IsAnimationEnabled = true;
    }*/
}