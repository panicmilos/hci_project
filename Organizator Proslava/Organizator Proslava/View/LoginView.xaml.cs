using System.Windows;
using System.Windows.Controls;

namespace Organizator_Proslava.View
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UserControl
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            pwtb.Text = pwb.Password;
        }
    }
}
