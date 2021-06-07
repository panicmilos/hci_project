﻿using Organizator_Proslava.Help;
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
            IInputElement focusedControl = FocusManager.GetFocusedElement(Application.Current.Windows[0]);
            if (focusedControl is DependencyObject dependencyObject)
            {
                string str = HelpProvider.GetHelpKey(dependencyObject);
                if (str != null)
                {
                    HelpProvider.ShowHelp(str, this);
                    return;
                }
            }

            HelpViewer helpViewer = new HelpViewer(DataContext.GetType(), this);
            helpViewer.Show();
        }
    }
}