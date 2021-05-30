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

namespace Organizator_Proslava.View.SharedComponents
{
    /// <summary>
    /// Interaction logic for DisabledTextAreaPreview.xaml
    /// </summary>
    public partial class DisabledTextAreaPreview : UserControl
    {
        public object Object
        {
            get { return (object)GetValue(ObjectProperty); }
            set { SetValue(ObjectProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ObjectProperty =
            DependencyProperty.Register("Object", typeof(object), typeof(DisabledTextAreaPreview), new PropertyMetadata(null));

        public DisabledTextAreaPreview()
        {
            InitializeComponent();
        }
    }
}