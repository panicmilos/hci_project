using System;
using System.Windows.Controls;
//using System.Windows.Input;

namespace Organizator_Proslava.View.CelebrationRequestForm
{
    public partial class CelebrationRequestInfoView : UserControl
    {
        public CelebrationRequestInfoView()
        {
            InitializeComponent();
            DateTimeFrom.MinDateTime = DateTime.Now.AddDays(2);
            DateTimeTo.MinDateTime = DateTime.Now.AddDays(2);
        }
        /*
        private void IntValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void FloatValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !float.TryParse(e.Text, out _);
        }
        */
    }
}