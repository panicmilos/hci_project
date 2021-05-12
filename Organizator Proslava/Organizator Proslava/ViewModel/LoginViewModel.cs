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

        public LoginViewModel()
        {
            User = new BaseUser();
            Login = new RelayCommand<BaseUser>(u => Trace.WriteLine(u.UserName));
            Register = new RelayCommand(() => EventBus.FireEvent("Register"));
        }
    }
}