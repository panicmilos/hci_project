using System;
using System.Globalization;
using System.Windows.Data;

namespace Organizator_Proslava.Converters
{
    public class StringToImageStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"pack://siteoforigin:,,,/Resources/{value}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}