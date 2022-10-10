using WriteToCompassion.Controls;
using System.Globalization;

namespace WriteToCompassion.Converters;

public class CloudAnimationTypeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {

        return (CloudAnimationType)value != 0;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }


}
