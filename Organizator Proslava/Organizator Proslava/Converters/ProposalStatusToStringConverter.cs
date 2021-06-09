using Organizator_Proslava.Model.CelebrationResponses;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Organizator_Proslava.Converters
{
    public class ProposalStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is CelebrationProposalStatus status)
            {
                switch (status)
                {
                    case CelebrationProposalStatus.Neobradjen: return "Neobrađen";
                    case CelebrationProposalStatus.Odbijen: return "Odbijen";
                    case CelebrationProposalStatus.Prihvacen: return "Prihvaćen";
                }
            }

            return String.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}