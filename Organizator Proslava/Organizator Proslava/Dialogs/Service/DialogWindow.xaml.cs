﻿using Organizator_Proslava.Help;
using System.Windows;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Service
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window, IDialogWindow
    {
        public DialogWindow()
        {
            InitializeComponent();
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
    }
}