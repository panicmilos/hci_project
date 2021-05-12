using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Alert
{
    public class AlertDialogViewModel : DialogViewModelBase<DialogResults>
    {
        public ICommand Ok { get; set; }

        public AlertDialogViewModel(string title, string message) :
            base(title, message)
        {
            Ok = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, DialogResults.Undefined));
        }
    }
}