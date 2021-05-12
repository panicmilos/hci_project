using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel
{
    public class MainViewModel : ObservableEntity
    {
        private object _currentViewModel;
        public object CurrentViewModel { get => _currentViewModel; set => OnPropertyChanged(ref _currentViewModel, value); }

        public LoginViewModel Lvm { get; set; }
        public RegisterViewModel Rvm { get; set; }

        public MainViewModel(LoginViewModel lvm, RegisterViewModel rvm)
        {
            Lvm = lvm;
            Rvm = rvm;

            CurrentViewModel = Lvm;
            EventBus.RegisterHandler("BackToLogin", () => CurrentViewModel = Lvm);
            EventBus.RegisterHandler("Register", () => CurrentViewModel = Rvm);
        }
    }
}