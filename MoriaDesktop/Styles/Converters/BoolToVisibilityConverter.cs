﻿using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace MoriaDesktop.Styles.Converters;
public class BoolToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b && b)
            return Visibility.Visible;
        else
            return Visibility.Collapsed;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is Visibility vis)
        {
            if (vis == Visibility.Visible)
                return true;
        }
        return false;
    }
}