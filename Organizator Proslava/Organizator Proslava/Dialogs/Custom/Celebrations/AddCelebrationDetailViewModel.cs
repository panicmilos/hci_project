using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Utility;
using System.Windows.Input;
using Organizator_Proslava.Model;
using System.ComponentModel;
using Organizator_Proslava.ViewModel.Utils;

namespace Organizator_Proslava.Dialogs.Custom.Celebrations
{
    public class AddCelebrationDetailViewModel : DialogViewModelBase<CelebrationDetail>, IDataErrorInfo
    {
        // Text fields:
        public string DetailTitle { get; set; }
        public string Content { get; set; }
        
        // Commands:
        public ICommand Add { get; set; }
        public ICommand Close { get; set; }

        // Rules:
        public string Error => throw new System.NotImplementedException();
        public string this[string columnName]
        {
            get
            {
                var valueOfProperty = GetType().GetProperty(columnName)?.GetValue(this);
                return Err(ValidationDictionary.Validate(columnName, valueOfProperty, null));
            }
        }

        private readonly IDialogService _dialogService;
        private int _calls = 0;

        public AddCelebrationDetailViewModel(CelebrationDetail celebrationDetail, IDialogService dialogService) :
            base("Dodavanje zahteva", 560, 460)
        {
            _dialogService = dialogService;
            DetailTitle = celebrationDetail?.Title;
            Content = celebrationDetail?.Content;
            
            Add = new RelayCommand<IDialogWindow>(w =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel
                    ($"Pitanje", $"Da li ste sigurni da želite da {(celebrationDetail?.Content == null ? "dodate" : "izmenite")} ovaj zahtev?")
                    ) == DialogResults.Yes)
                    CloseDialogWithResult(w, new CelebrationDetail { Title = DetailTitle, Content = Content });
            });

            Close = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, null));
        }

        private string Err(string message)
        {
            return message == null ? null : (_calls++ < 2 ? "*" : message);
        }
    }
}