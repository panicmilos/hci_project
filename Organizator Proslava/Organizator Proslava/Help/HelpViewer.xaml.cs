using Organizator_Proslava.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Organizator_Proslava.Help
{
    /// <summary>
    /// Interaction logic for HelpViewer.xaml
    /// </summary>
    public partial class HelpViewer : Window
    {
        private JavaScriptControlHelper ch;

        public HelpViewer(string helpKey, Window originator)
        {
            InitializeComponent();

            Uri u = new Uri($"http://proslavio-doc.bjelicaluka.com/{GetUrlForHelpKey(helpKey)}");
            ShowHelp(u, originator);
        }

        public HelpViewer(Type dataContextType, Window originator)
        {
            InitializeComponent();

            Uri u = new Uri($"http://proslavio-doc.bjelicaluka.com/{GetUrlForContext(dataContextType)}");
            ShowHelp(u, originator);
        }

        private void ShowHelp(Uri u, Window originator)
        {
            ch = new JavaScriptControlHelper(originator);
            wbHelp.ObjectForScripting = ch;
            wbHelp.Navigate(u);
        }

        private string GetUrlForHelpKey(string helpKey)
        {
            var keysToUrlDictionary = new Dictionary<string, string>()
            {
                { "Login", "user/auth.html#prijava-na-sistem" }
            };

            return keysToUrlDictionary.ContainsKey(helpKey) ? keysToUrlDictionary[helpKey] : "";
        }

        private string GetUrlForContext(Type dataContextType)
        {
            var contextsToUrlDictionary = new Dictionary<Type, string>()
            {
                { typeof(RegisterViewModel), "user/auth.html#kreiranje-naloga" }
            };

            return contextsToUrlDictionary.ContainsKey(dataContextType) ? contextsToUrlDictionary[dataContextType] : "";
        }

        private void BrowseBack_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (wbHelp != null) && wbHelp.CanGoBack;
        }

        private void BrowseBack_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            wbHelp.GoBack();
        }

        private void BrowseForward_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (wbHelp != null) && wbHelp.CanGoForward;
        }

        private void BrowseForward_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            wbHelp.GoForward();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }

        private void wbHelp_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
        }
    }
}