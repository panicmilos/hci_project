using Organizator_Proslava.Help;
using Organizator_Proslava.UserCommands;
using Organizator_Proslava.Utility;
using System.Diagnostics;
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

            var context = (DataContext as NavigabileModelView).Current;
            var helpViewer = context is NavigabileModelView navigabileModelView
                ? new HelpViewer(navigabileModelView.Current.GetType(), this)
                : new HelpViewer(context.GetType(), this);

            helpViewer.Show();
        }

        private void CommandBinding_Executed_Undo(object sender, ExecutedRoutedEventArgs e)
        {
            GlobalStore.ReadObject<IUserCommandManager>("userCommands").ExecuteUndo();
        }

        private void CommandBinding_Executed_Redo(object sender, ExecutedRoutedEventArgs e)
        {
            GlobalStore.ReadObject<IUserCommandManager>("userCommands").ExecuteRedo();
        }
    }
}