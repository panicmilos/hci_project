using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Utility;
using System.Windows.Input;
using Organizator_Proslava.Model;

namespace Organizator_Proslava.Dialogs.Custom.Celebrations
{
    public class AddCelebrationDetailViewModel : DialogViewModelBase<CelebrationDetail>
    {
        public CelebrationDetail CelebrationDetail { get; set; }

        public ICommand Add { get; set; }
        public ICommand Close { get; set; }

        private readonly IDialogService _dialogService;

        public AddCelebrationDetailViewModel(CelebrationDetail celebrationDetail, IDialogService dialogService) :
            base("Dodavanje zahteva", 560, 460)
        {
            _dialogService = dialogService;

            if (celebrationDetail == null)
            {
                CelebrationDetail = new CelebrationDetail();   
            }
            else
            {
                CelebrationDetail = new CelebrationDetail
                {
                    Content = celebrationDetail.Content,
                    Title = celebrationDetail.Title
                };
            }
            
            var word = celebrationDetail == null ? "dodate" : "izmenite"; 
            
            Add = new RelayCommand<IDialogWindow>(w =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel($"Pitanje", $"Da li ste sigurni da želite da {word} ovaj zahtev?")) == DialogResults.Yes)
                {
                    CloseDialogWithResult(w, CelebrationDetail);
                }
            });

            Close = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, null));
        }
    }
}