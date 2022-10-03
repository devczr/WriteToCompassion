using WriteToCompassion.Triggers;
using System;
using System.Timers;

namespace WriteToCompassion.Controls;


public partial class CustomCloudControl : ContentView
{

    public static readonly BindableProperty AllowCloudAnimationProperty =
        BindableProperty.Create(nameof(AllowCloudAnimation), typeof(bool), typeof(CustomCloudControl), false, propertyChanged: OnAllowCloudAnimationChanged);

    static async void OnAllowCloudAnimationChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is null) return;
        var cloudReportingChange = (CustomCloudControl)bindable;
        var val = (bool)newValue;

        if (val is true)
            await cloudReportingChange.BeginDrift();
        else if (val is false)
        {
            //can this throw an exception if no animation is running?
            //could check if AnimationIsRunning("Drift") before calling 
            cloudReportingChange.CancelAnimations();
        }
    }

    public CustomCloudControl()
    {
        PanGestureRecognizer panGesture = new PanGestureRecognizer();
        panGesture.PanUpdated += OnPanUpdated;
        this.GestureRecognizers.Add(panGesture);
        PanGestureTracker = -1;
        InitializeComponent();

    }
    public bool AllowCloudAnimation
    {
        get => (bool)GetValue(AllowCloudAnimationProperty);
        set => SetValue(AllowCloudAnimationProperty, value);
    }

    private int PanGestureTracker { get; set; }

    private double PanStartingX { get; set; }
    private double PanStartingY { get; set; }
    private double PanX { get; set; }
    private double PanY { get; set; }

    private bool PanTimeElapsed { get; set; }

    private System.Timers.Timer panTimer;


    private void SetTimer()
    {
        // Create a timer with a two second interval.
        panTimer = new System.Timers.Timer(50);
        // Hook up the Elapsed event for the timer. 
        panTimer.Elapsed += OnPanTimerFinished;
        panTimer.AutoReset = false;
        panTimer.Enabled = true;
    }

    private void OnPanTimerFinished(object sender, ElapsedEventArgs e)
    {
        PanTimeElapsed = true;
    }

    private async void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(sender);

        //gesture IDs start at 0, so PanGestureTracker is initialized to -1
        //checking the id avoids excessive calls to this.CancelAnimations
        if (e.GestureId != PanGestureTracker)
        {
            /*            var sendingCloud = (sender as CustomCloudControl);
                        PanStartingX = sendingCloud.TranslationX;
                        PanStartingY = sendingCloud.TranslationY;*/
            this.AllowCloudAnimation = false;
            PanGestureTracker = e.GestureId;
        }
        else
        {
            if ((e.StatusType is GestureStatus.Running) && PanTimeElapsed == true)
            {
                this.TranslationX = e.TotalX;
                this.TranslationY = e.TotalY;
                Shell.Current.DisplayAlert("pan timer elapse", $"x: {PanStartingX} \n y: {PanStartingY}", "ok");
            }
/*            else if ((e.StatusType == GestureStatus.Running) && !PanTimeElapsed)
            {

            }
*/            else if (e.StatusType == GestureStatus.Started)
            {
                this.SetTimer();
                this.TranslationX = (PanStartingX + e.TotalX);
                this.TranslationY = (PanStartingY + e.TotalY);
            }
            else if (e.StatusType == GestureStatus.Completed)
            {
                this.TranslationX += e.TotalX;
                PanX = this.TranslationX;

                this.TranslationY += e.TotalY;
                PanY = this.TranslationY;
                this.ShortHoverAnimation();
            }
        }
    }


    /*        if (deviceInfo.Platform == DevicePlatform.Android)
            {
                label.TranslationX += e.TotalX;
                label.TranslationY += e.TotalY;
            }
            else
            {
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

    public async Task ShortHoverAnimation()
    {
        PanStartingX = this.TranslationX;
        PanStartingY = this.TranslationY;
        await this.TranslateTo(PanX - 5, PanY - 5, 500);
        await this.TranslateTo(PanX - 5, PanY, 500);
        await this.TranslateTo(PanX, PanY - 5, 500);
        await this.TranslateTo(PanX, PanY, 500);

        PanStartingX = this.TranslationX;
        PanStartingY = this.TranslationY;

        this.AllowCloudAnimation = true;

    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {

        var parentCloud = (sender as Element)?.Parent as CustomCloudControl;

        var testDetails = parentCloud.AutomationId;
        Shell.Current.DisplayAlert("border tap", $"autoid: {testDetails}", "ok");

    }
}
