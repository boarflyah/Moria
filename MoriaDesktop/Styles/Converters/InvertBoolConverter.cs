using System.Globalization;
using System.Windows.Data;

namespace MoriaDesktop.Styles.Converters;
public class InvertBoolConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b && b)
            return false;

        return true;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b && b)
            return false;

        return true;
    }
}
