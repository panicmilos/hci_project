using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Organizator_Proslava.Converters
{
    public class PriceToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is float price)
            {
                return price + " RSD";
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}