using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Ninject;
using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class MainViewModel : NavigabileModelView
    {
        public LoginViewModel Lvm { get; set; }
        public RegisterViewModel Rvm { get; set; }
        public ClientHomeViewModel Chvm { get; set; }
        public OrganizerHomeViewModel Ohvm { get; set; }
        public AdminHomeViewModel Ahvm { get; set; }

        public ICommand OpenDemo { get; set; }

        public MainViewModel(LoginViewModel lvm)
        {
            Lvm = lvm;
            OpenDemo = new RelayCommand(() => new DemoService().OpenDemo(Current.GetType()));

            Switch(lvm);

            RegisterHandlers();
        }

        private void RegisterHandlers()
        {
            EventBus.RegisterHandler("SwitchMainViewModel", vm => Switch(vm));

            EventBus.RegisterHandler("AdminLogin", () =>
            {
                if (Ahvm == null)
                    Ahvm = ServiceLocator.Get<AdminHomeViewModel>();
                Switch(Ahvm);
            });

            EventBus.RegisterHandler("ClientLogin", () =>
            {
                if (Chvm == null)
                    Chvm = ServiceLocator.Get<ClientHomeViewModel>();
                Switch(Chvm);
            });

            EventBus.RegisterHandler("OrganizerLogin", () =>
            {
                if (Ohvm == null)
                    Ohvm = ServiceLocator.Get<OrganizerHomeViewModel>();
                Switch(Ohvm);
            });

            EventBus.RegisterHandler("BackToLogin", () =>
            {
                EventBus.Clear();
                Chvm = null;
                Ohvm = null;
                Ahvm = null;
                Rvm = null;
                Switch(Lvm);
                RegisterHandlers();
            });

            EventBus.RegisterHandler("Register", () =>
            {
                if (Rvm == null)
                    Rvm = ServiceLocator.Get<RegisterViewModel>();
                Switch(Rvm);
            });

            EventBus.RegisterHandler("BackToClientPage", () => Switch(Chvm));
        }
    }
}