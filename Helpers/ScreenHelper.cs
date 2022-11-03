
namespace WriteToCompassion.Helpers;

public static class ScreenHelper
{

    public static double CloudGridXValue { get; set; }
    public static double CloudGridYValue { get; set; }

    public static double LottieYValue { get; set; }

    public static double ContentXValue { get; set; }
    public static double ContentYValue { get; set; }


    public static void UpdateScreenXYValues(double x, double y, double contentX, double contentY, double lottieHeight = 0)
    {
        CloudGridXValue = x;
        CloudGridYValue = y;
        ContentXValue = contentX;
        ContentYValue = contentY;
        LottieYValue = lottieHeight;
    }

}
