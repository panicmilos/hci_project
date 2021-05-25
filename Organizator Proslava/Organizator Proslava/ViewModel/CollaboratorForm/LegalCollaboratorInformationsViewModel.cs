using Organizator_Proslava.Dialogs.Map;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CollaboratorForm
{
    public class LegalCollaboratorInformationsViewModel
    {
        public Collaborator Collaborator { get; set; }

        public ICommand OpenMap { get; set; }
        public ICommand Back { get; set; }
        public ICommand Next { get; set; }

        private IDialogService _dialogService;

        public LegalCollaboratorInformationsViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            Collaborator = new LegalCollaborator();

            Back = new RelayCommand(() => EventBus.FireEvent("BackToSelectCollaboratorType"));

            OpenMap = new RelayCommand(() =>
            {
                var address = _dialogService.OpenDialog(new MapDialogViewModel("Odaberi svoju adresu"));
                Collaborator.Address = address;
            });

            Next = new RelayCommand(() => EventBus.FireEvent("NextToCollaboratorServicesFromLegal"));
        }

        public void ForAdd()
        {
            Collaborator = new LegalCollaborator();
            Back = new RelayCommand(() => EventBus.FireEvent("BackToSelectCollaboratorType"));
        }

        public void ForUpdate(Collaborator collaborator)
        {
            Collaborator = collaborator;
            Back = new RelayCommand(() => EventBus.FireEvent("BackToCollaboratorsTable"));
        }
    }
}