using Organizator_Proslava.Dialogs.Alert;
using Organizator_Proslava.Dialogs.Map;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using System.Diagnostics;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class LoginViewModel
    {
        public BaseUser User { get; set; }
        public ICommand Login { get; set; }
        public ICommand Register { get; set; }
        public ICommand Map { get; set; }
        public ICommand Space { get; set; }
        public ICommand Isvm { get; set; }
        public ICommand Cfvm { get; set; }

        public LoginViewModel()
        {
            User = new BaseUser();
            Login = new RelayCommand<BaseUser>(u => Trace.WriteLine(u.UserName));
            Register = new RelayCommand(() => EventBus.FireEvent("Register"));
            Space = new RelayCommand(() => EventBus.FireEvent("Space")); // Delete Later
            Isvm = new RelayCommand(() => EventBus.FireEvent("Isvm")); // Delete Later
            Cfvm = new RelayCommand(() => EventBus.FireEvent("Cfvm")); // Delete Later

            Map = new RelayCommand(() =>
            {
                var result = new DialogService().OpenDialog(new MapDialogViewModel("Odaberi lokaciju"));
                var choosen = result == null ? "Nista" : $"{result.WholeAddress} ${result.Lat} ${result.Lng}";
                new DialogService().OpenDialog(new AlertDialogViewModel("Izabrao si", choosen));
            }); // Delete Later
        }
    }
}