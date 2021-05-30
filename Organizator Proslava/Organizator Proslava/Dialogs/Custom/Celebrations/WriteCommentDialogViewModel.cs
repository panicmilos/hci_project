using Organizator_Proslava.Dialogs.Option;
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

        private readonly IDialogService _dialogService;

        public WriteCommentDialogViewModel(IDialogService dialogService) :
            base("Dodavanje komentara", 560, 460)
        {
            _dialogService = dialogService;

            Add = new RelayCommand<IDialogWindow>(w =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Pitanje", "Da li ste sigurni da želite da komentarišete ovu ponudu?")) == DialogResults.Yes)
                {
                    CloseDialogWithResult(w, Comment);
                }
            });

            Close = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, null));
        }
    }
}