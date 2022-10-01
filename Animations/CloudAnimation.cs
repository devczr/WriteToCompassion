
using WriteToCompassion.Controls;
using WriteToCompassion.Services;
using WriteToCompassion.ViewModels;

namespace WriteToCompassion.Animations;

public class CloudAnimation : AnimationBase
{
    AnimationService animationService = new();


    protected override Task BeginAnimation()
    {

        if (Target == null)
        {
            throw new NullReferenceException("Null Animation Target property.");
        }

        Target.Dispatcher.Dispatch(() => Target.Animate("Drift", Drift(), 16, Duration, finished: async (value, wasCancelled) =>
        {
            if (wasCancelled || !RepeatForever) return;
            await this.Reset();
            await this.Begin();
        }));

        return Task.CompletedTask;
    }


    internal Animation Drift()
    {
        animationService.SetRandomDriftTranslationTargets(out double x, out double y, out uint durationRnd);
        this.Duration = durationRnd;
        var animation = new Animation();

        animation.WithConcurrent(
              (f) => Target.TranslationY = f, Target.TranslationY, y, Microsoft.Maui.Easing.SinInOut, 0, 1);
        animation.WithConcurrent(
              (f) => Target.TranslationX = f, Target.TranslationX, x, Microsoft.Maui.Easing.SinInOut, 0, 1);

        return animation;
    }

    protected override Task ResetAnimation()
    {
        if (Target == null)
        {
            throw new NullReferenceException("Null Target property.");
        }

        Target.Dispatcher.Dispatch(() => Target.TranslateTo(0, 0, 1000, Microsoft.Maui.Easing.SinInOut));

        return Task.CompletedTask;
    }


}
