using Organizator_Proslava.Utility;

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

        public MainViewModel(LoginViewModel lvm, RegisterViewModel rvm, SpaceViewModel svm, ImageSelectViewModel isvm)
        {
            Lvm = lvm;
            Rvm = rvm;
            Svm = svm;
            Isvm = isvm;

            CurrentViewModel = Lvm;
            EventBus.RegisterHandler("BackToLogin", () => CurrentViewModel = Lvm);
            EventBus.RegisterHandler("Register", () => CurrentViewModel = Rvm);
            EventBus.RegisterHandler("Space", () => CurrentViewModel = Svm); // Delete later
            EventBus.RegisterHandler("Isvm", () => CurrentViewModel = isvm); // Delete Later
        }
    }
}