using Organizator_Proslava.Dialogs.Alert;
using Organizator_Proslava.Dialogs.Map;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class LoginViewModel
    {
        public BaseUser User { get; set; }
        public ICommand Login { get; set; }
        public ICommand Register { get; set; }
        public ICommand Map { get; set; }
        public ICommand Isvm { get; set; }
        public ICommand DEMO { get; set; }
        public ICommand Colabs { get; set; }
        public ICommand Covm { get; set; }
        public ICommand ClientHome { get; set; }
        public ICommand OrgHome { get; set; }

        private readonly IUserService<BaseUser> _userService;
        private readonly IDialogService _dialogService;

        public IEnumerable<BaseUser> Users { get; set; } = new BaseUser[]
        {
            new Administrator
            {
                UserName = "admin",
                Role = Role.Administrator
            },
            new Organizer
            {
                UserName = "org",
                Role = Role.Organizer
            },
            new Client
            {
                UserName = "client",
                Role = Role.User
            },
        };

        public LoginViewModel(IUserService<BaseUser> userService, IDialogService dialogService)
        {
            _userService = userService;
            _dialogService = dialogService;
            User = new BaseUser();
            Login = new RelayCommand<BaseUser>(u =>
            {
                //var user = Users.FirstOrDefault(existingUser => existingUser.UserName == u.UserName);
                var user = _userService.Authenticate(u.UserName, u.Password);
                if (user == null)
                {
                    _dialogService.OpenDialog(new AlertDialogViewModel("", "Pogresno korisnicko ime ili sifra"));
                    return;
                }
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
            DEMO = new RelayCommand(() => EventBus.FireEvent("DEMO")); // Delete Later
            Colabs = new RelayCommand(() => EventBus.FireEvent("NextToCollaboratorsTable")); // Delete Later
            Covm = new RelayCommand(() => EventBus.FireEvent("CreateOrganizer")); // Delete later
            ClientHome = new RelayCommand(() => EventBus.FireEvent("ClientLogin")); // Delete later
            OrgHome = new RelayCommand(() => EventBus.FireEvent("OrganizerLogin")); // Delete later

            Map = new RelayCommand(() =>
            {
                var result = _dialogService.OpenDialog(new MapDialogViewModel("Odaberi lokaciju"));
                var chosen = result == null ? "Nista" : $"{result.WholeAddress} ${result.Lat} ${result.Lng}";
                _dialogService.OpenDialog(new AlertDialogViewModel("Izabrao si", chosen));
            }); // Delete Later
        }
    }
}