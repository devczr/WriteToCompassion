
using Microsoft.Maui.Devices;

namespace WriteToCompassion.Helpers;

public class PanHelper : ContentView
{

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
                Content.TranslationX = e.TotalX;
                Content.TranslationY = e.TotalY;
                break;

            case GestureStatus.Completed:
                Content.TranslationX += e.TotalX;
                Content.TranslationY += e.TotalY;
                break;
        }

    }
}