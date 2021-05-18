using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Organizator_Proslava.Converters
{
    public class YesNoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool bValue)
            {
                if (bValue)
                {
                    return "Da";
                }
                return "Ne";
            }

            return "Ne";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value.ToString() == "Da")
            {
                return true;
            }
            return false;
        }
    }
}