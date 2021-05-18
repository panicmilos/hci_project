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
        public ImageSelectViewModel Isvm { get; set; }
        public CollaboratorFormViewModel Cfvm { get; set; }

        public MainViewModel(LoginViewModel lvm, RegisterViewModel rvm, SpaceViewModel svm, ImageSelectViewModel isvm, CollaboratorFormViewModel cfvm)
        {
            Lvm = lvm;
            Rvm = rvm;
            Svm = svm;
            Isvm = isvm;
            Cfvm = cfvm;

            CurrentViewModel = Lvm;
            EventBus.RegisterHandler("BackToLogin", () => CurrentViewModel = Lvm);
            EventBus.RegisterHandler("Register", () => CurrentViewModel = Rvm);
            EventBus.RegisterHandler("Space", () => CurrentViewModel = Svm); // Delete later
            EventBus.RegisterHandler("Isvm", () => CurrentViewModel = isvm); // Delete Later
            EventBus.RegisterHandler("Cfvm", () => CurrentViewModel = cfvm); // Delete Later
        }
    }
}