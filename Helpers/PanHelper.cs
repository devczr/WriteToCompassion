
namespace WriteToCompassion.Helpers;

public class PanHelper : ContentView
{
    double x, y;

    public PanHelper()
    {
        PanGestureRecognizer panGesture = new PanGestureRecognizer();
        panGesture.PanUpdated += OnPanUpdated;
        GestureRecognizers.Add(panGesture);
    }

    void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
//        var myPanHelper = (sender as Element)?.Parent as PanHelper;
        //       Shell.Current.DisplayAlert("PanUpdated", "OK", "OK");
        
        switch (e.StatusType)
        {
            case GestureStatus.Running:
                // Translate and ensure we don't pan beyond the wrapped user interface element bounds.
                Content.TranslationX = x + e.TotalX;
                Content.TranslationY = y + e.TotalY;
                break;

            case GestureStatus.Completed:
                // Store the translation applied during the pan
                x = Content.TranslationX;
                y = Content.TranslationY;
                break;
        }

    }
}