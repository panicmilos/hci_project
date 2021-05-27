using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Celebrations
{
    public class WriteCommentDialogViewModel : DialogViewModelBase<string>
    {
        public string Comment { get; set; }

        public ICommand Add { get; set; }
        public ICommand Close { get; set; }

        public WriteCommentDialogViewModel() :
            base("Dodavanje komentara", 560, 460)
        {
            Add = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, Comment));
            Close = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, null));
        }
    }
}