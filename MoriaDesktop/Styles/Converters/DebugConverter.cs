using System.Globalization;
using System.Windows.Data;

namespace MoriaDesktop.Styles.Converters;
public class DebugConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        ;
        return value;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
}
