using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace MoriaDesktop.Styles.Converters;
public class DueDateToBackgroundMultiConverter : IMultiValueConverter
{
    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values.Length < 2 || values[0] == null || values[1] == null)
            return Brushes.White;

        if (values[0] is DateTime dueDate && values[1] is DateTime selectedDay)
        {
            int weekNumber = culture.Calendar.GetWeekOfYear(selectedDay, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            DateTime startOfWeek = ISOWeek.ToDateTime(DateTime.Now.Year, weekNumber, DayOfWeek.Monday);
            DateTime endOfWeek = startOfWeek.AddDays(6);

            return dueDate >= startOfWeek && dueDate <= endOfWeek
                ? Brushes.White
                : new SolidColorBrush(Color.FromRgb(240, 240, 240)); // Bardzo jasnoszary
        }

        return new SolidColorBrush(Color.FromRgb(240, 240, 240));
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
