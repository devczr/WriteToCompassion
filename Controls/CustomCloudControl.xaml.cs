
namespace WriteToCompassion.Controls;


public partial class CustomCloudControl : ContentView
{


    public static readonly BindableProperty AllowCloudAnimationProperty =
        BindableProperty.Create(nameof(AllowCloudAnimation), typeof(bool), typeof(CustomCloudControl), false, BindingMode.TwoWay);

    public CustomCloudControl()
    {
        InitializeComponent();
        Initialize();
    }
    public bool AllowCloudAnimation
    {
        get => (bool)GetValue(AllowCloudAnimationProperty);
        set => SetValue(AllowCloudAnimationProperty, value);
    }

    private void Initialize()
    {
        PanGestureRecognizer panGesture = new PanGestureRecognizer();
        panGesture.PanUpdated += OnPanUpdated;
        this.GestureRecognizers.Add(panGesture);
    }

    private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {

            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    TranslationX = e.TotalX;
                    TranslationY = e.TotalY;
                    break;

                case GestureStatus.Completed:
                    double x = TranslationX;
                    double y = TranslationY;
                    break;
            }
    }

}
