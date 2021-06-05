using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Organizator_Proslava.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool boolean)
            {
                return boolean ? Visibility.Visible : Visibility.Collapsed;
            }

            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility visible)
            {
                return visible == Visibility.Visible;
            }

            return false;
        }
    }
}