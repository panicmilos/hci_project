using System;
using System.Collections.Generic;
using Organizator_Proslava.Dialogs.Alert;
using Organizator_Proslava.Dialogs.Map;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using System.Diagnostics;
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
        public ICommand Space { get; set; }
        public ICommand Isvm { get; set; }
        public ICommand Cfvm { get; set; }

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

        public LoginViewModel()
        {
            User = new BaseUser();
            Login = new RelayCommand<BaseUser>(u =>
            {
                var user = Users.FirstOrDefault(existingUser => existingUser.UserName == u.UserName);
                if (user == null) return;
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
            Space = new RelayCommand(() => EventBus.FireEvent("Space")); // Delete Later
            Isvm = new RelayCommand(() => EventBus.FireEvent("Isvm")); // Delete Later
            Cfvm = new RelayCommand(() => EventBus.FireEvent("Cfvm")); // Delete Later

            Map = new RelayCommand(() =>
            {
                var result = new DialogService().OpenDialog(new LargeDialogWindow(), new MapDialogViewModel("Odaberi lokaciju"));
                var chosen = result == null ? "Nista" : $"{result.WholeAddress} ${result.Lat} ${result.Lng}";
                new DialogService().OpenDialog(new AlertDialogViewModel("Izabrao si", chosen));

            }); // Delete Later
        }
    }
}