
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
        AnimationService animationService = new();
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
}