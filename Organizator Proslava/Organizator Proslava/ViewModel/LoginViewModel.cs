using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using System.Diagnostics;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class LoginViewModel
    {
        public User User { get; set; }
        public ICommand Login { get; set; }
        public ICommand Register { get; set; }

        public LoginViewModel()
        {
            User = new User();
            Login = new RelayCommand<User>(u => Trace.WriteLine(u.Name));
            Register = new RelayCommand(() => EventBus.FireEvent("Register"));
        }
    }
}