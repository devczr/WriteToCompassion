
using System.Threading.Tasks;

namespace WriteToCompassion.Controls;


public partial class CustomCloudControl : ContentView
{
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
        switch (newCloudAnimationValue)
        {
            case CloudAnimationType.None:
                cloudReportingChange.CancelAnimations();
                break;

            case CloudAnimationType.Drift:
                await cloudReportingChange.DriftAround();
                break;

            case CloudAnimationType.Hover:
                await cloudReportingChange.LocalHover();
                break;

            case CloudAnimationType.Dance:
                await cloudReportingChange.Dance();
                break;
        }
    }


    AnimationService cloudAnimationService;
    public CustomCloudControl()
    {
        /*      TODO: implement pangestures to drag clouds around screen
         *      code commented out at bottom
                PanGestureRecognizer panGesture = new PanGestureRecognizer();
                panGesture.PanUpdated += OnPanUpdated;
                this.GestureRecognizers.Add(panGesture);
                PanGestureTracker = -1;*/



        AnimationService animationService = new AnimationService();
        InitializeComponent();
        cloudAnimationService = animationService;
    }


    /*    public async Task DriftAround()
        {
            cloudAnimationService.SetRandomDriftTranslationTargets(out double x, out double y, out uint durationRandom);

            await this.TranslateTo(x, y, durationRandom, easing: Easing.SinInOut).ContinueWith(
                async antecedent =>
                {
                    await this.DriftAround();

                }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }*/

    public async Task DriftAround()
    {
        this.CancelAnimations();
        cloudAnimationService.SetRandomDriftTranslationTargets(out double x, out double y, out uint durationRandom);
        do
        {
            await this.TranslateTo(x, y, durationRandom, easing: Easing.SinInOut);
            cloudAnimationService.SetRandomDriftTranslationTargets(out x, out y, out durationRandom);

        } while (this.CloudAnimation == CloudAnimationType.Drift);
    }

    public async Task Dance()
    {
        this.CancelAnimations();
        var startingScale = this.Scale;
        await this.ScaleTo(startingScale * 1.5, 250, Easing.BounceOut);
        await this.ScaleTo(startingScale / 2, 250, Easing.BounceIn);
        await this.ScaleTo(startingScale, 100, Easing.SpringIn);
        await Task.Delay(250);
        this.CloudAnimation = CloudAnimationType.Hover;
    }

    public async Task LocalHover()
    {
        var startingX = this.TranslationX;
        var startingY = this.TranslationY;
        do
        {
            this.CancelAnimations();
            await this.TranslateTo(startingX - 5, startingY - 5, 1000);
            await this.TranslateTo(startingX - 5, startingY, 1000);
            await this.TranslateTo(startingX, startingY - 5, 1000);
            await this.TranslateTo(startingX, startingY, 1000);
        } while (this.CloudAnimation == CloudAnimationType.Hover);
    }


    private async void viewclicked(object sender, EventArgs e)
    {
        await Shell.Current.DisplayAlert("click", "clack", "whack");

    }

    private async void PanGestureRecognizer_PanUpdated(object sender, PanUpdatedEventArgs e)
    {
        if(e.StatusType is GestureStatus.Started)
        {
            this.CloudAnimation = CloudAnimationType.None;
        }

    }

    private void DragGestureRecognizer_DragStarting(object sender, DragStartingEventArgs e)
    {
        e.Data.Text = 
    
    }
}
/*    private int PanGestureTracker { get; set; }

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

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {

        var parentCloud = (sender as Element)?.Parent as CustomCloudControl;

        var testDetails = parentCloud.AutomationId;
        Shell.Current.DisplayAlert("border tap", $"autoid: {testDetails}", "ok");

    }
*/
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
