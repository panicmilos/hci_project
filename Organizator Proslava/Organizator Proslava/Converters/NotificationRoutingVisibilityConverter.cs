using Organizator_Proslava.Model;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Organizator_Proslava.Converters
{
    public class NotificationRoutingVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CanceledResponseNotification)
            {
                return Visibility.Collapsed;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}