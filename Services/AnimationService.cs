﻿
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Controls;

namespace WriteToCompassion.Services;

public partial class AnimationService : ObservableObject
{
    //TODO: do these need to be observable properties or can they be made private?
    //leaving as observable for now for easy debugging
    [ObservableProperty]
    double topCloudBoundary = -200;

    [ObservableProperty]
    double bottomCloudBoundary = 0.1;

    [ObservableProperty]
    double leftCloudBoundary = -200;

    [ObservableProperty]
    double rightCloudBoundary = 200;


    private static double ScreenXValue { get; set; }
    private static double ScreenYValue { get; set; }

    private static readonly Random rnd = new();



    public void UpdateScreenXYValues(double x, double y)
    {
        ScreenXValue = x;
        ScreenYValue = y;
    }

    async Task SetVerticalBoundaries()
    {
        double yBoundary = ScreenYValue;

        //subtracting bottom grid cell set to 50 units
        yBoundary -= 50;

        // 5*, * grid sizes for first two cells means 6 total units
        yBoundary /= 6;

        // * units in the clouds grid cell (5)
        yBoundary *= 5;

        //subtracting aesthetic buffer
        yBoundary -= 70;



        try
        {
            //clouds should only translate upwards from starting position, so we need negative values only
            TopCloudBoundary = Math.Round(yBoundary, 1, MidpointRounding.AwayFromZero) * -1;

            //clouds are set to VerticalOptions "End" so 0 is the lowest they should go
            //bottom boundary already set to 0 as default value
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Cloud Boundary Error",
                $"{ex.Message}", "OK");
        }


    }


    async Task SetHorizontalBoundaries()
    {
        //Clouds are set to HorizontalOptions Center, so the middle is 0, to the right border is half the width

        double xBoundary = ScreenXValue / 2;

        //subtracting aesthetic buffer
        xBoundary -= 50;

        try
        {
            RightCloudBoundary = Math.Round(xBoundary, 0, MidpointRounding.AwayFromZero);

            //the left border is the negative value of the right border
            LeftCloudBoundary = (Math.Round(xBoundary, 0, MidpointRounding.AwayFromZero) * -1);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Cloud Boundary Error",
                $"{ex.Message}", "OK");
        }


    }


    public void SetRandomDriftTranslationTargets(out double x, out double y, out uint durationRnd)
    {
        SetHorizontalBoundaries();
        SetVerticalBoundaries();

        x = rnd.NextDouble() * (RightCloudBoundary - LeftCloudBoundary) + LeftCloudBoundary;
        y = rnd.NextDouble() * (TopCloudBoundary - BottomCloudBoundary) + BottomCloudBoundary;
        durationRnd = (uint)rnd.Next(5000, 7000);
        // TODO: add logic for more exciting cloud patterns
    }

}