using Microsoft.EntityFrameworkCore.ChangeTracking;
using Organizator_Proslava.Model;
using Organizator_Proslava.Ninject;
using Organizator_Proslava.Services.Contracts;
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
                if (!celebrationDetails1.Any())
                {
                    return Visibility.Hidden;
                }
                else
                {
                    if (ServiceLocator.Get<ICelebrationResponseService>().ReadForCelebration(celebrationDetails1.First().CelebrationId) == null)
                        return Visibility.Hidden;
                }
            }

            if (value is ObservableHashSet<CelebrationDetail> celebrationDetails2)
            {
                if (!celebrationDetails2.Any())
                {
                    return Visibility.Hidden;
                }
                else
                {
                    if (ServiceLocator.Get<ICelebrationResponseService>().ReadForCelebration(celebrationDetails2.First().CelebrationId) == null)
                        return Visibility.Hidden;
                }
            }

            if (value is ObservableCollection<CelebrationDetail> celebrationDetails3)
            {
                if (!celebrationDetails3.Any())
                {
                    return Visibility.Hidden;
                }
                else
                {
                    if (ServiceLocator.Get<ICelebrationResponseService>().ReadForCelebration(celebrationDetails3.First().CelebrationId) == null)
                        return Visibility.Hidden;
                }
            }

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}