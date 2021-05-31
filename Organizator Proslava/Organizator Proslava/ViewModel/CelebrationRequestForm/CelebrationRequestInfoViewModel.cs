using System.Windows.Input;
using Organizator_Proslava.Dialogs.Map;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel.CelebrationRequestForm
{
    public class CelebrationRequestInfoViewModel
    {
        public Celebration Celebration { get; set; }
        
        public ICommand OpenOrganizersDialog { get; set; }
        public ICommand OpenMap { get; set; }
        public ICommand Back { get; set; }
        public ICommand Next { get; set; }

        private readonly IDialogService _dialogService;

        public CelebrationRequestInfoViewModel(IOrganizerService organizerService, IDialogService dialogService)
        {
            _dialogService = dialogService;
            
            OpenOrganizersDialog = new RelayCommand(() =>
            {
                Celebration.Organizer = _dialogService.OpenDialog(new ChooseOrganizerViewModel(organizerService));
            });
            OpenMap = new RelayCommand(() =>
            {
                Celebration.Address = _dialogService.OpenDialog(new MapDialogViewModel("Odaberite adresu proslave"));
            });
            Back = new RelayCommand(() => EventBus.FireEvent("BackToClientPage"));
            Next = new RelayCommand(() => EventBus.FireEvent("NextToCelebrationRequestDetails"));
        }

        public void ForAdd()
        {
            Celebration = new Celebration();
        }
        
        public void ForUpdate(Celebration celebration)
        {
            Celebration = celebration;
        }
    }
}