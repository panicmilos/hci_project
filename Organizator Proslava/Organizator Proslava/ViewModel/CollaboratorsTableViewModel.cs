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
using Organizator_Proslava.Dialogs.Alert;

namespace Organizator_Proslava.ViewModel
{
    public class CollaboratorsTableViewModel : ObservableEntity
    {
        private string _parentSwitchViewModelEvent;
        public ObservableCollection<Collaborator> Collaborators { get; set; }

        public ICommand Back { get; set; }
        public ICommand Next { get; set; }

        public ICommand Add { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Remove { get; set; }
        public ICommand Details { get; set; }

        private readonly CollaboratorFormViewModel _cfvm;

        private readonly ICollaboratorService _collaboratorService;
        private readonly ICelebrationProposalService _celebrationProposalService;
        private readonly IDialogService _dialogService;
        public LegalCollaboratorDetailViewModel LegalCollaboratorDetailViewModel { get; set; }
        public IndividualCollaboratorDetailViewModel IndividualCollaboratorDetailViewModel { get; set; }
        public DisplayImagesViewModel DisplayImagesViewModel { get; set; }
        public CollaboratorServiceTableViewModel CollaboratorServiceTableViewModel { get; set; }
        public DisplayHallsDialogViewModel DisplayHallsDialogViewModel { get; set; }


        public CollaboratorsTableViewModel(CollaboratorFormViewModel cfvm, 
            ICollaboratorService collaboratorService,
            ICelebrationProposalService celebrationProposalService,
            IDialogService dialogService,
            LegalCollaboratorDetailViewModel legalCollaboratorDetailViewModel,
            IndividualCollaboratorDetailViewModel individualCollaboratorDetailViewModel,
            DisplayImagesViewModel displayImagesViewModel,
            CollaboratorServiceTableViewModel collaboratorServiceTableViewModel,
            DisplayHallsDialogViewModel displayHallsDialogViewModel)
        {
            _cfvm = cfvm;
            _collaboratorService = collaboratorService;
            _celebrationProposalService = celebrationProposalService;
            _dialogService = dialogService;

            LegalCollaboratorDetailViewModel = legalCollaboratorDetailViewModel;
            IndividualCollaboratorDetailViewModel = individualCollaboratorDetailViewModel;
            DisplayImagesViewModel = displayImagesViewModel;
            CollaboratorServiceTableViewModel = collaboratorServiceTableViewModel;
            DisplayHallsDialogViewModel = displayHallsDialogViewModel;

            Collaborators = new ObservableCollection<Collaborator>(_collaboratorService.Read());

            Add = new RelayCommand(() =>
            {
                EventBus.FireEvent("CollaboratorFormForAdd");
                EventBus.FireEvent(_parentSwitchViewModelEvent, _cfvm);
            });

            Edit = new RelayCommand<Collaborator>(collaborator =>
            {
                EventBus.FireEvent("CollaboratorFormForUpdate", collaborator);
                EventBus.FireEvent(_parentSwitchViewModelEvent, _cfvm);
            });

            Remove = new RelayCommand<Collaborator>(collaborator =>
            {
                if (!_celebrationProposalService.CanDeleteCollaborator(collaborator.Id))
                {
                    _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje",
                        "Nije moguće obrisati saradnika jer učestvuje u budućim proslavama."));
                } else if (_dialogService.OpenDialog(new OptionDialogViewModel("Potvrda",
                    "Da li ste sigurni da želite da obrišete ovog saradnika?")) == DialogResults.Yes)
                {
                    Collaborators.Remove(collaborator);
                    _collaboratorService.Delete(collaborator.Id);
                    GlobalStore.ReadObject<IUserCommandManager>("userCommands")
                        .Add(new DeleteCollaborator(collaborator));
                }
            });

            Details = new RelayCommand<Collaborator>(collaborator =>
            {
                /*if (collaborator is IndividualCollaborator)
                {
                    _dialogService.OpenDialog(new IndividualCollaboratorDetailViewModel(collaborator as IndividualCollaborator));
                }
                else
                {
                    _dialogService.OpenDialog(new LegalCollaboratorDetailViewModel(collaborator as LegalCollaborator));
                }*/
                DetailsCollaboratorDialogViewModel dcdvm = new DetailsCollaboratorDialogViewModel(LegalCollaboratorDetailViewModel, IndividualCollaboratorDetailViewModel,
                    DisplayImagesViewModel, CollaboratorServiceTableViewModel, DisplayHallsDialogViewModel);
                dcdvm.Collaborator = collaborator;
                dcdvm.DisplayInfoAboutCollaborator();
                _dialogService.OpenDialog(dcdvm);
            });

            EventBus.RegisterHandler("ReloadCollaboratorTable", () => { Collaborators.Clear(); _collaboratorService.Read().ToList().ForEach(c => Collaborators.Add(c)); });
        }

        public void ForOrganizer()
        {
            Back = new RelayCommand(() => EventBus.FireEvent("BackToCurrentCelebrationsForOrganizer"));
            _cfvm.Sctvm.BackTo = "BackToCollaboratorsForOrganizer";
            _cfvm.Icivm.BackTo = "BackToCollaboratorsForOrganizer";
            _cfvm.Lcivm.BackTo = "BackToCollaboratorsForOrganizer";
            _cfvm.Chvm.AsRole = "Organizer";
            _parentSwitchViewModelEvent = "SwitchOrganizerViewModel";
        }

        public void ForAdministrator()
        {
            Back = new RelayCommand(() => EventBus.FireEvent("AdminLogin"));
            _cfvm.Sctvm.BackTo = "BackToCollaboratorsForAdmin";
            _cfvm.Icivm.BackTo = "BackToCollaboratorsForAdmin";
            _cfvm.Lcivm.BackTo = "BackToCollaboratorsForAdmin";
            _cfvm.Chvm.AsRole = "Admin";
            _parentSwitchViewModelEvent = "SwitchMainViewModel";
        }
    }
}