using Organizator_Proslava.Model;
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

namespace Organizator_Proslava.View.RequestResponseFormShared
{
    /// <summary>
    /// Interaction logic for RequestPreview.xaml
    /// </summary>
    public partial class RequestPreview : UserControl
    {
        public Celebration Celebration
        {
            get { return (Celebration)GetValue(CelebrationProperty); }
            set { SetValue(CelebrationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CelebrationProperty =
            DependencyProperty.Register("Celebration", typeof(Celebration), typeof(RequestPreview), new PropertyMetadata(new Celebration()));

        public RequestPreview()
        {
            InitializeComponent();
        }
    }
}