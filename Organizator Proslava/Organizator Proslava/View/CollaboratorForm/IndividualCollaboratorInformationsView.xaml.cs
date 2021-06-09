using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Organizator_Proslava.View.CollaboratorForm
{
    /// <summary>
    /// Interaction logic for IndividualCollaboratorInformationsView.xaml
    /// </summary>
    public partial class IndividualCollaboratorInformationsView : UserControl
    {
        private bool _init = true;

        public IndividualCollaboratorInformationsView()
        {
            InitializeComponent();
        }

        private void pw1_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            pw01.Text = pw1.Password;
        }

        private void pw2_PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            pw02.Text = pw2.Password;
        }

        private void PasswordChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            if (_init)
            {
                pw1.Password = pw01.Text;
                pw2.Password = pw02.Text;
                _init = false;
            }
        }
    }
}
