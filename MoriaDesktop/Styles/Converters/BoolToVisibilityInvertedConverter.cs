using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MoriaDesktop.Styles.Converters;

public class BoolToVisibilityInvertedConverter: IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b && b)
            return Visibility.Collapsed;
        else
            return Visibility.Visible;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility vis)
        {
            if (vis == Visibility.Visible)
                return false;
        }
        return true;
    }
}
