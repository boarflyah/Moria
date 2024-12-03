using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using MoriaDesktop.Models.Enums;

namespace MoriaDesktop.Styles.Converters;

internal class SystemInfoStatusToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SystemInfoStatus status)
        {
            ResourceDictionary resourceDictionary = Application.Current.Resources.MergedDictionaries.FirstOrDefault(x => x.Source.OriginalString.Contains("ColorScheme"));

            switch (status)
            {
                case SystemInfoStatus.Info:
                    return (Brush)resourceDictionary["DefaultInfoBrush"];
                case SystemInfoStatus.Success:
                    return (Brush)resourceDictionary["DefaultSuccessBrush"];
                case SystemInfoStatus.Warning:
                    return (Brush)resourceDictionary["DefaultWarningBrush"];                    
                case SystemInfoStatus.Error:
                    return (Brush)resourceDictionary["DefaultErrorBrush"];
                default:
                    return new SolidColorBrush(Colors.Transparent);
            }
        }
        return default;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return SystemInfoStatus.None;
    }
}
