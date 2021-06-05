using Organizator_Proslava.Help;
using Organizator_Proslava.Utility;
using System.Windows;
using System.Windows.Input;

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

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject dependencyObject)
            {
                string str = HelpProvider.GetHelpKey(dependencyObject);
                HelpProvider.ShowHelp(str, this);
            }
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