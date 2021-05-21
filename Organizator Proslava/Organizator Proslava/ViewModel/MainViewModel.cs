using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CollaboratorForm;

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
        public ImageSelectViewModel Isvm { get; set; }
        public CollaboratorFormViewModel Cfvm { get; set; }

        public MainViewModel(LoginViewModel lvm, RegisterViewModel rvm
            , ClientHomeViewModel chvm, OrganizerHomeViewModel ohvm, AdminHomeViewModel ahvm, ImageSelectViewModel isvm, CollaboratorFormViewModel cfvm)
        {
            Lvm = lvm;
            Rvm = rvm;
            Chvm = chvm;
            Ohvm = ohvm;
            Ahvm = ahvm;
            Isvm = isvm;
            Cfvm = cfvm;

            CurrentViewModel = Lvm;
            EventBus.RegisterHandler("AdminLogin", () => CurrentViewModel = Ahvm);
            EventBus.RegisterHandler("ClientLogin", () => CurrentViewModel = Chvm);
            EventBus.RegisterHandler("OrganizerLogin", () => CurrentViewModel = Ohvm);
            EventBus.RegisterHandler("BackToLogin", () => CurrentViewModel = Lvm);
            EventBus.RegisterHandler("Register", () => CurrentViewModel = Rvm);
            EventBus.RegisterHandler("Isvm", () => CurrentViewModel = isvm); // Delete Later
            EventBus.RegisterHandler("Cfvm", () => CurrentViewModel = cfvm); // Delete Later
        }
    }
}