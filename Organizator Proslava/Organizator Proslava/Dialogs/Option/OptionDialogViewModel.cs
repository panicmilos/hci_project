using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Option
{
    public class OptionDialogViewModel : DialogViewModelBase<DialogResults>
    {
        public ICommand Yes { get; set; }
        public ICommand No { get; set; }

        public OptionDialogViewModel(string title, string message) :
            base(title, message)
        {
            Yes = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, DialogResults.Yes));
            No = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, DialogResults.No));
        }
    }
}