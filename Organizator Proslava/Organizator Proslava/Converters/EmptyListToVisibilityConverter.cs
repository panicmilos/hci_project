using Microsoft.EntityFrameworkCore.ChangeTracking;
using Organizator_Proslava.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Organizator_Proslava.Converters
{
    public class EmptyListToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Trace.WriteLine(value.GetType());

            if (value is List<CelebrationDetail> celebrationDetails1)
            {
                return celebrationDetails1.Any() ? Visibility.Visible : Visibility.Hidden;
            }

            if (value is ObservableHashSet<CelebrationDetail> celebrationDetails2)
            {
                return celebrationDetails2.Any() ? Visibility.Visible : Visibility.Hidden;
            }

            if (value is ObservableCollection<CelebrationDetail> celebrationDetails3)
            {
                return celebrationDetails3.Any() ? Visibility.Visible : Visibility.Hidden;
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}