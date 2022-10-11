
using CommunityToolkit.Maui.Alerts;
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace WriteToCompassion.Controls;


public partial class CustomCloudControl : ContentView
{

    public static readonly BindableProperty CloudAnimationProperty =
 BindableProperty.Create(nameof(CloudAnimation), typeof(CloudAnimationType), typeof(CustomCloudControl), CloudAnimationType.None, propertyChanged: OnCloudAnimationChanged);

    public static readonly BindableProperty CloudControlIDProperty = BindableProperty.Create(nameof(CloudControlID), typeof(Guid), typeof(CustomCloudControl));

    public Guid CloudControlID
    {
        get => (Guid)GetValue(CloudControlIDProperty);
        set => SetValue(CloudControlIDProperty, value);
    }

    public CloudAnimationType CloudAnimation
    {
        get => (CloudAnimationType)GetValue(CloudAnimationProperty);
        set => SetValue(CloudAnimationProperty, value);
    }

    //Note: CloudAnimationType should not be changed directly inside this code-behind
    //doing so would successfully change the control, but the observable collection in the
    //viewmodel would be out of sync
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

            case CloudAnimationType.HoverToDrift:
                await cloudReportingChange.HoverToDrift();
                break;

            case CloudAnimationType.Dance:
                await cloudReportingChange.Dance();
                break;

            case CloudAnimationType.Display:
                await cloudReportingChange.MoveToContentArea();
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

    public async Task DriftAround()
    {
        cloudAnimationService.SetRandomDriftTranslationTargets(out double x, out double y, out uint durationRandom);
        while (this.CloudAnimation == CloudAnimationType.Drift)
        {
            await this.TranslateTo(x, y, durationRandom, easing: Easing.Linear);
            cloudAnimationService.SetRandomDriftTranslationTargets(out x, out y, out durationRandom);
        }
    }

    public async Task Dance()
    {
        this.CancelAnimations();
        var startingScale = this.Scale;
        await this.ScaleTo(startingScale * 1.5, 250, Easing.BounceOut);
        await this.ScaleTo(startingScale / 2, 250, Easing.BounceIn);
        await this.ScaleTo(startingScale, 100, Easing.SpringIn).ContinueWith(antecedent =>
        {
            this.ScaleTo(startingScale);
        });

    }




    public async Task MoveToContentArea()
    {
        //x should be 0 as that's the middle of the view
        //targetY set based on the HomeView xaml controls for cloudGrid and border that holds the text content. several factors causing cloud misalignment visually, so subtracting 65
        var targetY = ((ScreenHelper.CloudGridYValue + (ScreenHelper.ContentYValue / 2)) - 65) * -1;


        //different animations based on the control's horizontal position relative to center of the screen
        if (this.TranslationX > 0)
        {
            await Task.WhenAll
            (
                this.TranslateTo(0, targetY, 1000),
                this.FadeTo(0, 1100, Easing.BounceIn),
                this.RotateTo(360, 1000, Easing.Linear),
                this.ScaleTo(1.1, 1000, Easing.CubicIn)
            );
        }
        else
        {
            await Task.WhenAll
            (
                this.TranslateTo(0, targetY, 1000),
                this.FadeTo(0, 1100, Easing.BounceIn),
                this.RotateTo(-360, 1000, Easing.Linear),
                this.ScaleTo(1.1, 1000, Easing.CubicIn)
            );
        }

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

    public async Task HoverToDrift()
    {
        var startingX = this.TranslationX;
        var startingY = this.TranslationY;

        this.CancelAnimations();
        await this.TranslateTo(startingX - 5, startingY - 5, 1000);
        await this.TranslateTo(startingX - 5, startingY, 1000);
        await this.TranslateTo(startingX, startingY - 5, 1000);
        await this.TranslateTo(startingX, startingY, 1000);
        await Task.Run(() => this.CloudAnimation = CloudAnimationType.Drift);

    }


}