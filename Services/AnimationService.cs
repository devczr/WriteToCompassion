using WriteToCompassion.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;

namespace WriteToCompassion.Services;

public partial class AnimationService : ObservableObject
{
    [ObservableProperty]
    int topCloudBoundary = -200;

    [ObservableProperty]
    int bottomCloudBoundary = -10;

    [ObservableProperty]
    int leftCloudBoundary = -200;

    [ObservableProperty]
    int rightCloudBoundary = 200;

    private static readonly Random rnd = new();

    private void SetVerticalBoundaries()
    {

        double yBoundary = ScreenHelper.ScreenYValue;

        try
        {
            // ratio of space for Grid.Row = 1
            yBoundary *= .75;
            //subtract 70 for aesthetic buffer
            yBoundary -= 20;

 

            //clouds should only translate upwards from starting position, so we need negative values only
            TopCloudBoundary = (int)Math.Round(yBoundary, 0, MidpointRounding.AwayFromZero) * -1;

            //subtracting 50 from height of lottie to allow clouds to drift slighty under the top of the lottie
            BottomCloudBoundary = ((int)ScreenHelper.LottieYValue - 50) * -1;

        }
        catch (Exception ex)
        {
            Shell.Current.DisplayAlert("Cloud Boundary Error",
                $"{ex.Message}", "OK");
            TopCloudBoundary = -200;
            BottomCloudBoundary = -10;
        }


    }


    private void SetHorizontalBoundaries()
    {
        //Clouds are set to HorizontalOptions Center, so the middle is 0, to the right border is half the width

        double xBoundary = ScreenHelper.ScreenXValue / 2;

        //subtracting aesthetic buffer
        xBoundary -= 50;

        try
        {
            RightCloudBoundary = (int)Math.Round(xBoundary, 0, MidpointRounding.AwayFromZero);

            //the left border is the negative value of the right border
            LeftCloudBoundary = (int)(Math.Round(xBoundary, 0, MidpointRounding.AwayFromZero) * -1);
        }
        catch (Exception ex)
        {
            Shell.Current.DisplayAlert("Cloud Boundary Error",
                $"{ex.Message}", "OK");
        }


    }


    public void SetRandomDriftTranslationTargets(out double x, out double y, out uint durationRnd)
    {
        SetHorizontalBoundaries();
        SetVerticalBoundaries();

        //       x = rnd.NextDouble() * (RightCloudBoundary - LeftCloudBoundary) + LeftCloudBoundary;
        //       y = rnd.NextDouble() * (TopCloudBoundary - BottomCloudBoundary) + BottomCloudBoundary;
        if (RightCloudBoundary > LeftCloudBoundary)
            x = rnd.Next(LeftCloudBoundary, RightCloudBoundary);
        else
            x = 0;

        if (BottomCloudBoundary > TopCloudBoundary)
            y = rnd.Next(TopCloudBoundary, BottomCloudBoundary);
        else
            y = -50;

        durationRnd = (uint)rnd.Next(5000, 7000);
        // TODO: add logic for more exciting cloud patterns
    }

}
