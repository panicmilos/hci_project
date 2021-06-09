using Organizator_Proslava.Dialogs.Custom.Notifications;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Ninject;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CelebrationRequestForm;
using Organizator_Proslava.ViewModel.Celebrations;
using Organizator_Proslava.ViewModel.UsersView;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class MainViewModel : ObservableEntity
    {
        private object _currentViewModel;
        public object CurrentViewModel { get => _currentViewModel; set => OnPropertyChanged(ref _currentViewModel, value); }

        public LoginViewModel Lvm { get; set; }
        public RegisterViewModel Rvm { get; set; }
        public ClientHomeViewModel Chvm { get; set; }
        public OrganizerHomeViewModel Ohvm { get; set; }
        public AdminHomeViewModel Ahvm { get; set; }

        public ICommand OpenDemo { get; set; }

        public MainViewModel(
            LoginViewModel lvm,
            RegisterViewModel rvm)
        {
            Lvm = lvm;
            Rvm = rvm;

            CurrentViewModel = lvm;
            EventBus.RegisterHandler("SwitchMainViewModel", vm => CurrentViewModel = vm);

            EventBus.RegisterHandler("AdminLogin", () =>
            {
                if (Ahvm == null)
                    Ahvm = ServiceLocator.Get<AdminHomeViewModel>();
                CurrentViewModel = Ahvm;
            });

            EventBus.RegisterHandler("ClientLogin", () =>
            {
                if (Chvm == null)
                    Chvm = ServiceLocator.Get<ClientHomeViewModel>();
                CurrentViewModel = Chvm;
            });

            EventBus.RegisterHandler("OrganizerLogin", () =>
            {
                if (Ohvm == null)
                    Ohvm = ServiceLocator.Get<OrganizerHomeViewModel>();
                CurrentViewModel = Ohvm;
            });

            EventBus.RegisterHandler("BackToLogin", () =>
            {
                Chvm = null;
                Ohvm = null;
                Ahvm = null;
                CurrentViewModel = Lvm;
            });

            EventBus.RegisterHandler("Register", () => CurrentViewModel = Rvm);

            EventBus.RegisterHandler("BackToClientPage", () => CurrentViewModel = Chvm);

            OpenDemo = new RelayCommand(() => new DemoService().OpenDemo(CurrentViewModel.GetType()));
        }
    }
}