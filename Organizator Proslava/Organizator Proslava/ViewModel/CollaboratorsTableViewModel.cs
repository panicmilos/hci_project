using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Custom.Collaborators;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.UserCommands;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CollaboratorForm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class CollaboratorsTableViewModel : ObservableEntity
    {
        public ObservableCollection<Collaborator> Collaborators { get; set; }

        public ICommand Back { get; set; }
        public ICommand Next { get; set; }

        public ICommand Add { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Remove { get; set; }
        public ICommand Details { get; set; }

        private readonly CollaboratorFormViewModel _cfvm;

        private readonly ICollaboratorService _collaboratorService;
        private readonly IDialogService _dialogService;

        public CollaboratorsTableViewModel(CollaboratorFormViewModel cfvm, ICollaboratorService collaboratorService, IDialogService dialogService)
        {
            _cfvm = cfvm;
            _collaboratorService = collaboratorService;
            _dialogService = dialogService;

            Collaborators = new ObservableCollection<Collaborator>(_collaboratorService.Read());

            Add = new RelayCommand(() =>
            {
                EventBus.FireEvent("CollaboratorFormForAdd");
                EventBus.FireEvent("SwitchMainViewModel", _cfvm);
            });

            Edit = new RelayCommand<Collaborator>(collaborator =>
            {
                EventBus.FireEvent("CollaboratorFormForUpdate", collaborator);
                EventBus.FireEvent("SwitchMainViewModel", _cfvm);
            });

            Remove = new RelayCommand<Collaborator>(collaborator =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Pitanje", "Da li ste sigurni da želite da obrišete ovog saradnika?")) == DialogResults.Yes)
                {
                    Collaborators.Remove(collaborator);
                    _collaboratorService.Delete(collaborator.Id);
                    GlobalStore.ReadObject<IUserCommandManager>("userCommands").Add(new DeleteCollaborator(collaborator));
                }
            });

            Details = new RelayCommand<Collaborator>(collaborator =>
            {
                if (collaborator is IndividualCollaborator)
                {
                    _dialogService.OpenDialog(new IndividualCollaboratorDetailViewModel(collaborator as IndividualCollaborator));
                }
                else
                {
                    _dialogService.OpenDialog(new LegalCollaboratorDetailViewModel(collaborator as LegalCollaborator));
                }
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToLogin"));

            EventBus.RegisterHandler("ReloadCollaboratorTable", () => { Collaborators.Clear(); _collaboratorService.Read().ToList().ForEach(c => Collaborators.Add(c)); });
        }
    }
}