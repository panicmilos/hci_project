using System.Windows.Controls;

namespace Organizator_Proslava.View
{
    /// <summary>
    /// Interaction logic for RegisterView.xaml
    /// </summary>
    public partial class RegisterView : UserControl
    {
        private bool _init = true;

        public RegisterView()
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