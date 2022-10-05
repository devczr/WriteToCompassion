
namespace WriteToCompassion.Helpers
{
    public static class ScreenHelper
    {

        public static double ScreenXValue { get; set; }
        public static double ScreenYValue { get; set; }


        public static void UpdateScreenXYValues(double x, double y)
        {
            ScreenXValue = x;
            ScreenYValue = y;
        }

    }
}
