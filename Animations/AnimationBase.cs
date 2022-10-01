

namespace WriteToCompassion.Animations;

public abstract class AnimationBase : BindableObject
{
    private bool _isAnimating = false;

    public static readonly BindableProperty TargetProperty = BindableProperty.Create(nameof(Target), typeof(VisualElement), typeof(AnimationBase), null,
        propertyChanged: (bindable, oldValue, newValue) => ((AnimationBase)bindable).Target = (VisualElement)newValue);


    public static readonly BindableProperty DurationProperty = BindableProperty.Create(nameof(Duration), typeof(uint), typeof(AnimationBase), (uint)1000,
propertyChanged: (bindable, oldValue, newValue) => ((AnimationBase)bindable).Duration = (uint)newValue);


    public static readonly BindableProperty RepeatForeverProperty = BindableProperty.Create(nameof(RepeatForever), typeof(bool), typeof(AnimationBase), false,
        propertyChanged: (bindable, oldValue, newValue) => ((AnimationBase)bindable).RepeatForever = (bool)newValue);

    public static readonly BindableProperty DelayProperty = BindableProperty.Create(nameof(Delay), typeof(int), typeof(AnimationBase), 0,
    propertyChanged: (bindable, oldValue, newValue) => ((AnimationBase)bindable).Delay = (int)newValue);

    public static readonly BindableProperty EasingProperty = BindableProperty.Create(nameof(Easing), typeof(EasingType), typeof(AnimationBase), EasingType.SinInOut,
    propertyChanged: (bindable, oldValue, newValue) => ((AnimationBase)bindable).Easing = (EasingType)newValue);

    public VisualElement Target
    {
        get => (VisualElement)GetValue(TargetProperty);
        set => SetValue(TargetProperty, value);
    }

    public uint Duration
    {
        get => (uint)GetValue(DurationProperty);
        set => SetValue(DurationProperty, value);
    }

    public bool RepeatForever
    {
        get => (bool)GetValue(RepeatForeverProperty);
        set => SetValue(RepeatForeverProperty, value);
    }

    public int Delay
    {
        get => (int)GetValue(DelayProperty);
        set => SetValue(DelayProperty, value);
    }

    public EasingType Easing
    {
        get => (EasingType)GetValue(EasingProperty);
        set => SetValue(EasingProperty, value);
    }

    protected abstract Task BeginAnimation();


    public async Task Begin()
    {
        try
        {
            if (!_isAnimating)
            {
                _isAnimating = true;

                await InternalBegin()
                    .ContinueWith(t => t.Exception, TaskContinuationOptions.OnlyOnFaulted).ConfigureAwait(false);
            }
        }
        catch (TaskCanceledException)
        {
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Exception in animation {ex}", "OK");
        }
    }

    protected abstract Task ResetAnimation();



    public Task Reset()
    {
        _isAnimating = false;
        return Task.CompletedTask;
        //await ResetAnimation();
    }

    private async Task InternalBegin()
    {
        if (Delay > 0)
        {
            await Task.Delay(Delay);
        }

            await BeginAnimation();
    }
}



