using Organizator_Proslava.Dialogs.Custom.Demo;
using System;

namespace Organizator_Proslava.Dialogs.Service
{
    internal class DemoService : IDemoService
    {
        public void OpenDemo(Type dataContextType)
        {
            IDialogWindow window = new DemoWindow
            {
                DataContext = new DemoDialogViewModel(dataContextType)
            };

            _ = window.ShowDialog();

            return;
        }
    }
}