
using WriteToCompassion.Services;


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
    AnimationService cloudAnimationService;
    public CustomCloudControl()
    {
        PanGestureRecognizer panGesture = new PanGestureRecognizer();
        panGesture.PanUpdated += OnPanUpdated;
        this.GestureRecognizers.Add(panGesture);
        PanGestureTracker = -1;
        AnimationService animationService = new AnimationService();
        InitializeComponent();
        cloudAnimationService = animationService;
    }
    public bool AllowCloudAnimation
    {
        get => (bool)GetValue(AllowCloudAnimationProperty);
        set => SetValue(AllowCloudAnimationProperty, value);
    }

    private int PanGestureTracker { get; set; }

    private double PanFinalX { get; set; }
    public double PanFinalY { get; set; }

     private async void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        ArgumentNullException.ThrowIfNull(sender);

        //gesture IDs start at 0, so PanGestureTracker is initialized to -1
        //checking the id avoids excessive calls to this.CancelAnimations
        if (e.GestureId != PanGestureTracker)
        {

            this.AllowCloudAnimation = false;
            PanGestureTracker = e.GestureId;
        }
        else
        {
            if (e.StatusType is GestureStatus.Running)
            {
                this.TranslationX = e.TotalX;
                this.TranslationY = e.TotalY;
            }
            else if (e.StatusType is GestureStatus.Completed)
            {
                this.TranslationX += e.TotalX;
                this.TranslationY += e.TotalY;
                PanFinalX = this.TranslationX;
                PanFinalY = this.TranslationY;
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
        cloudAnimationService.SetRandomDriftTranslationTargets(out double x, out double y, out uint durationRnd);


        //        var randomDriftAnimation = new Animation(v => this.TranslateTo(x, y, durationRnd, Easing.SinInOut));

        var parentAnimation = new Animation();
        var randomDriftXAnimation = new Animation(v => this.TranslationX = v, this.TranslationX, x, Easing.SinInOut, null); 
        var randomDriftYAnimation = new Animation(v => this.TranslationY = v, this.TranslationY, y, Easing.SinInOut, null);

        //       var driftAnimation = new Animation(v => this.TranslationX = v,0,1,Easing.Linear);
        parentAnimation.Add(0, 1, randomDriftXAnimation);
        parentAnimation.Add(0, 1, randomDriftYAnimation);
        parentAnimation.Commit(this, "ChildAnimations", 16, durationRnd);

        /*await this.TranslateTo(-100, 0, 2500);    // Move image left
        await this.TranslateTo(-100, -100, 2500); // Move image diagonally up and left
        await this.TranslateTo(100, 100, 2500);   // Move image diagonally down and right
        await this.TranslateTo(0, 100, 2500);     // Move image left
        await this.TranslateTo(0, 0, 2500);       // Move image up*/
    }

    public async Task ShortHoverAnimation()
    {

        await this.TranslateTo(PanFinalX - 5, PanFinalY - 5, 500);
        await this.TranslateTo(PanFinalX - 5, PanFinalY, 500);
        await this.TranslateTo(PanFinalX, PanFinalY - 5, 500);
        await this.TranslateTo(PanFinalX, PanFinalY, 500);
        this.AllowCloudAnimation = true;

    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {

        var parentCloud = (sender as Element)?.Parent as CustomCloudControl;

        var testDetails = parentCloud.AutomationId;
        Shell.Current.DisplayAlert("border tap", $"autoid: {testDetails}", "ok");

    }


}
