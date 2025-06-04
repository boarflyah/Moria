using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using MoriaModelsDo.Base.Enums;

namespace MoriaDesktop.Styles.Converters;

public class RowBackgroundTaskStateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is SystemOrderState state)
        {
            switch (state)
            {
                case SystemOrderState.MachineWiredAndTested:
                    return Brushes.LightGreen;
                case SystemOrderState.MachineReleased:
                    return Brushes.PowderBlue;
                default:
                    return DependencyProperty.UnsetValue;
            }
        }

        return DependencyProperty.UnsetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
