
namespace WriteToCompassion.Controls;

public class CloudButton : ImageButton
{
    public static readonly BindableProperty CloudImageProperty =
    BindableProperty.Create(nameof(CloudImage), typeof(ImageSource), typeof(CloudButton), null);

    public static readonly BindableProperty AnimateDriftProperty =
        BindableProperty.Create(nameof(AnimateDrift), typeof(bool), typeof(CloudButton), false);




    public CloudButton()
    {
        Initialize();
    }
    public bool AnimateDrift
    {
        get => (bool)GetValue(AnimateDriftProperty);
        set => SetValue(AnimateDriftProperty, value);
    }

    public ImageSource CloudImage
    {
        get => (ImageSource)GetValue(CloudImageProperty);
        set => SetValue(CloudImageProperty, value);
    }


    private void Initialize()
    {

        AnimateDrift = true;
    }


}
