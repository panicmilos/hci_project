using Organizator_Proslava.Help;
using Organizator_Proslava.Utility;
using System.Windows;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Service
{
    /// <summary>
    /// Interaction logic for DemoWindow.xaml
    /// </summary>
    public partial class DemoWindow : Window, IDialogWindow
    {
        public DemoWindow()
        {
            InitializeComponent();
            EventBus.RegisterHandler("DemoFullscreenMode", EnterFullscreenMode);
            EventBus.RegisterHandler("ExitDemoFullscreenMode", ExitFullscreenMode);
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject dependencyObject)
            {
                string str = HelpProvider.GetHelpKey(dependencyObject);
                if (str != null)
                {
                    HelpProvider.ShowHelp(str, this);
                    return;
                }
            }

            var helpViewer = DataContext is NavigabileModelView navigabileModelView
                ? new HelpViewer(navigabileModelView.Current.GetType(), this)
                : new HelpViewer(DataContext.GetType(), this);
            helpViewer.Show();
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