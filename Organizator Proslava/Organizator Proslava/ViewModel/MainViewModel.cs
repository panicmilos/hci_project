using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CollaboratorForm;
using Organizator_Proslava.ViewModel.DemoForm;

namespace Organizator_Proslava.ViewModel
{
    public class MainViewModel : ObservableEntity
    {
        private object _currentViewModel;
        public object CurrentViewModel { get => _currentViewModel; set => OnPropertyChanged(ref _currentViewModel, value); }

        public LoginViewModel Lvm { get; set; }
        public RegisterViewModel Rvm { get; set; }
        public SpaceViewModel Svm { get; set; }
        public ClientHomeViewModel Chvm { get; set; }
        public OrganizerHomeViewModel Ohvm { get; set; }

        public AdminHomeViewModel Ahvm { get; set; }
        public CollaboratorFormViewModel Cfvm { get; set; }
        public CollaboratorsTableViewModel Ctvm { get; set; }
        public CreateOrganizerViewModel Covm { get; set; }

        public MainViewModel(
            LoginViewModel lvm,
            RegisterViewModel rvm,
            ClientHomeViewModel chvm,
            OrganizerHomeViewModel ohvm,
            AdminHomeViewModel ahvm,
            CollaboratorFormViewModel cfvm,
            CollaboratorsTableViewModel ctvm, 
            CreateOrganizerViewModel covm)
        {
            Lvm = lvm;
            Rvm = rvm;
            Chvm = chvm;
            Ohvm = ohvm;
            Ahvm = ahvm;
            Cfvm = cfvm;
            Ctvm = ctvm;
            Covm = covm;

            CurrentViewModel = Lvm;
            EventBus.RegisterHandler("AdminLogin", () => CurrentViewModel = Ahvm);
            EventBus.RegisterHandler("ClientLogin", () => CurrentViewModel = Chvm);
            EventBus.RegisterHandler("OrganizerLogin", () => CurrentViewModel = Ohvm);
            EventBus.RegisterHandler("BackToLogin", () => CurrentViewModel = Lvm);
            EventBus.RegisterHandler("Register", () => CurrentViewModel = Rvm);
            EventBus.RegisterHandler("Cfvm", () => CurrentViewModel = cfvm); // Delete Later
            EventBus.RegisterHandler("DEMO", () => CurrentViewModel = new DemoViewModel()); // Delete Later
            EventBus.RegisterHandler("NextToCollaboratorsTable", () => CurrentViewModel = ctvm); // Delete Later

            EventBus.RegisterHandler("BackToCollaboratorsTable", () => CurrentViewModel = ctvm); // Delete Later
            EventBus.RegisterHandler("CreateOrganizer", () => CurrentViewModel = covm); // Delete later
        }
    }
}