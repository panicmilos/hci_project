using Organizator_Proslava.Dialogs.Alert;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.DTO;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class LoginViewModel
    {
        public LoginDTO LoginDTO { get; set; }
        public ICommand Login { get; set; }
        public ICommand Register { get; set; }
        public ICommand Isvm { get; set; }
        public ICommand Covm { get; set; }
        public ICommand ClientHome { get; set; }
        public ICommand OrgHome { get; set; }

        private readonly IUserService<BaseUser> _userService;
        private readonly IDialogService _dialogService;

        public LoginViewModel(IUserService<BaseUser> userService, IDialogService dialogService)
        {
            _userService = userService;
            _dialogService = dialogService;
            LoginDTO = new LoginDTO();
            Login = new RelayCommand<LoginDTO>(login =>
            {
                var user = _userService.Authenticate(login.UserName, login.Password);
                if (user == null)
                {
                    _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje", "Pogrešno korisničko ime ili šifra."));
                    return;
                }
                LoginDTO.Password = string.Empty;
                GlobalStore.AddObject("loggedUser", user);
                switch (user.Role)
                {
                    case Role.Administrator:
                        EventBus.FireEvent("AdminLogin");
                        break;

                    case Role.Organizer:
                        EventBus.FireEvent("OrganizerLogin");
                        break;

                    case Role.Collaborator:
                        break;

                    case Role.User:
                        EventBus.FireEvent("ClientLogin");
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            });
            Register = new RelayCommand(() => EventBus.FireEvent("Register"));
            Covm = new RelayCommand(() => EventBus.FireEvent("AdminLogin")); // Delete later
            ClientHome = new RelayCommand(() => EventBus.FireEvent("ClientLogin")); // Delete later
            OrgHome = new RelayCommand(() => EventBus.FireEvent("OrganizerLogin")); // Delete later
        }
    }
}