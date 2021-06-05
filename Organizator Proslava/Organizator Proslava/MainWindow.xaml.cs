using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Organizator_Proslava
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            EventBus.RegisterHandler("DemoFullscreenMode", EnterFullscreenMode);
            EventBus.RegisterHandler("ExitDemoFullscreenMode", ExitFullscreenMode);
        }

        private void EnterFullscreenMode()
        {
            WindowStyle = WindowStyle.None;
            WindowState = WindowState.Maximized;
        }

        private void ExitFullscreenMode()
        {
            WindowStyle = WindowStyle.SingleBorderWindow;
            WindowState = WindowState.Normal;
        }
    }
}