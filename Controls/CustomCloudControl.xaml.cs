
using Microsoft.Maui.Devices;
using WriteToCompassion.Triggers;

namespace WriteToCompassion.Controls;


public partial class CustomCloudControl : ContentView
{


    public static readonly BindableProperty AllowCloudAnimationProperty =
        BindableProperty.Create(nameof(AllowCloudAnimation), typeof(bool), typeof(CustomCloudControl), false, propertyChanged: OnAllowCloudAnimationChanged);

    static void OnAllowCloudAnimationChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if(bindable is null) return;
        var cloudReportingChange = (CustomCloudControl)bindable;
        var val = (bool)newValue;

        if (val)
            cloudReportingChange.BeginDrift();
        else if (!val)
            cloudReportingChange.CancelAnimations();

        Shell.Current.DisplayAlert(" ok", $"type is: {val}", "ok");
    }

    public CustomCloudControl()
    {
        InitializeComponent();
        Initialize();
    }
    public bool AllowCloudAnimation
    {
        get => (bool)GetValue(AllowCloudAnimationProperty);
        set => SetValue(AllowCloudAnimationProperty, value);
    }



    private void Initialize()
    {
/*        PanGestureRecognizer panGesture = new PanGestureRecognizer();
        panGesture.PanUpdated += OnPanUpdated;
        this.GestureRecognizers.Add(panGesture);*/
    }

/*    private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(sender);

        //        Shell.Current.DisplayAlert("Pan", "Ok", "Ok");
        this.CancelAnimations();


        *//*        if (deviceInfo.Platform == DevicePlatform.Android)
                {
                    label.TranslationX += e.TotalX;
                    label.TranslationY += e.TotalY;
                }
                else
                {
        *//*

        switch (e.StatusType)
        {
            case GestureStatus.Running:
                this.TranslationX += e.TotalX;
                this.TranslationY += e.TotalY;
                break;
            case GestureStatus.Completed:
                this.TranslationX += e.TotalX;
                this.TranslationY += e.TotalY;
                break;
        }

    }*/


    public async Task BeginDrift()
    {
        //  var animation = new Animation(v=> this.TranslateTo(-50,-50))
        await this.TranslateTo(-100, 0, 2500);    // Move image left
        await this.TranslateTo(-100, -100, 2500); // Move image diagonally up and left
        await this.TranslateTo(100, 100, 2500);   // Move image diagonally down and right
        await this.TranslateTo(0, 100, 2500);     // Move image left
        await this.TranslateTo(0, 0, 2500);       // Move image up
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {

        var parentCloud = (sender as Element)?.Parent as CustomCloudControl;

        var testDetails = parentCloud.AutomationId;
        Shell.Current.DisplayAlert("border tap", $"autoid: {testDetails}", "ok");

    }
}
