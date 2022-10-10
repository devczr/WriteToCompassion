
using Microsoft.Maui.Graphics;
using System.Threading.Tasks;

namespace WriteToCompassion.Controls;


public partial class CustomCloudControl : ContentView
{
    public static int loopCounter = 0;



    public static readonly BindableProperty CloudAnimationProperty =
 BindableProperty.Create(nameof(CloudAnimation), typeof(CloudAnimationType), typeof(CustomCloudControl), CloudAnimationType.None, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnCloudAnimationChanged);

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

    public async Task DriftAround()
    {
        this.CancelAnimations();
        cloudAnimationService.SetRandomDriftTranslationTargets(out double x, out double y, out uint durationRandom);
        await this.TranslateTo(x, y, 5000, easing: Easing.SinInOut);

        loopCounter++;
        loopLabel.Text = loopCounter.ToString();
        while (true)
        {

            await this.TranslateTo(x, y, 5000, easing: Easing.SinInOut);
            loopCounter++;
            loopLabel.Text = loopCounter.ToString();

            cloudAnimationService.SetRandomDriftTranslationTargets(out x, out y, out durationRandom);

        }
    }

    /*    public async Task DriftAround()
        {
            bool colorBool = false;
            this.CancelAnimations();
            cloudAnimationService.SetRandomDriftTranslationTargets(out double x, out double y, out uint durationRandom);
            loopCounter++;
            loopLabel.Text = loopCounter.ToString();
            do
            {
                loopCounter++;
                loopLabel.Text = loopCounter.ToString();
                await this.TranslateTo(x, y, 5000, easing: Easing.SinInOut);
                if(colorBool)
                    this.BackgroundColor = Colors.BlueViolet;
                else
                    this.BackgroundColor = Colors.Red;
                colorBool = !colorBool;
                cloudAnimationService.SetRandomDriftTranslationTargets(out x, out y, out durationRandom);

            } while (this.CloudAnimation == CloudAnimationType.Drift);
        }*/

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