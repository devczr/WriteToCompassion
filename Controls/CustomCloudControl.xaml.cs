
using WriteToCompassion.Services;


namespace WriteToCompassion.Controls;


public partial class CustomCloudControl : ContentView
{

    public enum CloudAnimationType
    {
        None,
        Drift,
        Hover
    }

    public static readonly BindableProperty CloudAnimationProperty =
     BindableProperty.Create(nameof(CloudAnimation), typeof(CloudAnimationType), typeof(CustomCloudControl), CloudAnimationType.None,
     propertyChanged: OnCloudAnimationChanged);

    public CloudAnimationType CloudAnimation
    {
        get => (CloudAnimationType)GetValue(CloudAnimationProperty);
        set => SetValue(CloudAnimationProperty, value);
    }

    static async void OnCloudAnimationChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is null) return;
        var cloudReportingChange = (CustomCloudControl)bindable;
        var newCloudAnimationValue = (CloudAnimationType)newValue;

        if(newCloudAnimationValue is CloudAnimationType.None)
        {
            cloudReportingChange.CancelAnimations();
        }
        else if(newCloudAnimationValue is CloudAnimationType.Drift)
        {
            cloudReportingChange.DriftAround();

        }
        else if (newCloudAnimationValue is CloudAnimationType.Hover)
        {
            cloudReportingChange.LocalHover();
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

    public async Task TestActionFinished()
    {
       await Shell.Current.DisplayAlert("action sent this", $"", "ok");

    }

    public async Task BeginDrift()
    {
        cloudAnimationService.SetRandomDriftTranslationTargets(out double x, out double y, out uint durationRnd);

        var animation = new Animation();

        animation.WithConcurrent((v) => this.TranslationX = v, this.TranslationX, x, Easing.SinInOut,0,1); 
        animation.WithConcurrent((v) => this.TranslationY = v, this.TranslationY, y, Easing.SinInOut, 0,1);

        animation.Commit(this, "driftanimation", 16,durationRnd,finished: (d,b) =>
        {

        });
    }

    public async Task DriftAround()
    {
        do
        {
            cloudAnimationService.SetRandomDriftTranslationTargets(out double x, out double y, out uint durationRnd);
            await this.TranslateTo(x, y, durationRnd, easing: Easing.SinInOut);

        } while (this.CloudAnimation == CloudAnimationType.Drift);
    }



    public async Task LocalHover()
    {
        var startingX = this.TranslationX;
        var startingY = this.TranslationY;

        do
        {
            await this.TranslateTo(startingX - 5, startingY - 5, 1000);
            await this.TranslateTo(startingX - 5, startingY, 1000);
            await this.TranslateTo(startingX, startingY - 5, 1000);
            await this.TranslateTo(startingX, startingY, 1000);
        } while (this.CloudAnimation == CloudAnimationType.Hover);
    }


    public async Task ShortHoverAnimation()
    {

        await this.TranslateTo(PanFinalX - 5, PanFinalY - 5, 500);
        await this.TranslateTo(PanFinalX - 5, PanFinalY, 500);
        await this.TranslateTo(PanFinalX, PanFinalY - 5, 500);
        await this.TranslateTo(PanFinalX, PanFinalY, 500);

    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {

        var parentCloud = (sender as Element)?.Parent as CustomCloudControl;

        var testDetails = parentCloud.AutomationId;
        Shell.Current.DisplayAlert("border tap", $"autoid: {testDetails}", "ok");

    }


}
