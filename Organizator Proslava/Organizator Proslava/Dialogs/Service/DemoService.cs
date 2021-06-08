using Organizator_Proslava.Dialogs.Custom.Demo;

namespace Organizator_Proslava.Dialogs.Service
{
    internal class DemoService : IDemoService
    {
        public void OpenDemo()
        {
            IDialogWindow window = new DemoWindow
            {
                DataContext = new DemoDialogViewModel()
            };

            _ = window.ShowDialog();

            return;
        }
    }
}