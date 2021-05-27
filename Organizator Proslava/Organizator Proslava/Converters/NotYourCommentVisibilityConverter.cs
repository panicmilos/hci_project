using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Organizator_Proslava.Converters
{
    public class NotYourCommentVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var writterComment = value as BaseUser;
            var loggedUser = GlobalStore.ReadObject<BaseUser>("loggedUser");

            return writterComment.Id != loggedUser.Id ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}