using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MovieApp.WPF.Helpers;
public class BoolToStarColorConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (bool)value ? Brushes.Gold : Brushes.Gray;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Binding.DoNothing;
    }
}
