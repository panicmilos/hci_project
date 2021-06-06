using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Alert;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class RegisterViewModel : IDataErrorInfo
    {
        // Text fields:
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MailAddress { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string RepeatedPassword { get; set; }
        // Commands:
        public ICommand Register { get; set; }
        public ICommand Back { get; set; }
        public bool ForEdit { get; set; }
        public string ButtonText { get; set; }
        // Rules:
        public string Error { get; set; }
        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "FirstName":
                        if (string.IsNullOrWhiteSpace(FirstName)) return Err("Morate zadati ime.");
                        break;
                    case "LastName":
                        if (string.IsNullOrWhiteSpace(LastName)) return Err("Morate zadati prezime.");
                        break;
                    case "MailAddress":
                        if (string.IsNullOrWhiteSpace(MailAddress)) return Err("Morate zadati mail adresu.");
                        if (!_email.IsValid(MailAddress)) return Err("Nevalidna mail adresa.");
                        break;
                    case "UserName":
                        if (string.IsNullOrWhiteSpace(UserName)) return Err("Morate zadati korisnicko ime.");
                        if (UserName.Trim().Length < 5) return Err("Korisnicko ime je prekratko.");
                        if (UserName.Trim().Length > 15) return Err("Korisnicko ime je predugacko.");
                        break;
                    case "PhoneNumber":
                        if (string.IsNullOrWhiteSpace(PhoneNumber)) return Err("Morate zadati broj telefona.");
                        //TODO: validate format 
                        break;
                    case "Password":
                        if (string.IsNullOrWhiteSpace(Password)) return Err("Morate kreirati sifru.");
                        if (Password != RepeatedPassword) return Err(" ");
                        break;
                    case "RepeatedPassword":
                        if (string.IsNullOrWhiteSpace(RepeatedPassword)) return Err("Morate ponoviti sifru.");
                        if (Password != RepeatedPassword) return Err("Nije ista kao sifra.");
                        break;
                    default:
                        return null;
                }
                return null;
            }
        }

        private readonly IClientService _clientService;
        private readonly IDialogService _dialogService;
        private readonly EmailAddressAttribute _email;
        private int _calls;

        public RegisterViewModel(IClientService clientService, IDialogService dialogService)
        {
            _clientService = clientService;
            _dialogService = dialogService;
            _email = new EmailAddressAttribute();
            _calls = 0;
            ForEdit = false;
            ButtonText = "Napravi nalog";

            Register = new RelayCommand(() =>
            {
                if (ForEdit) UpdateClient();
                else RegisterClient();
            });

            Back = new RelayCommand(() =>
            {
                _calls = 0;
                if (ForEdit) EventBus.FireEvent("ClientLogin");
                else EventBus.FireEvent("BackToLogin");
            });
        }

        private void RegisterClient()
        {
            if (_clientService.AlreadyInUse(UserName))
            {
                _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje", "Zadato korisnicko ime je vec iskorisceno"));
                return;
            }
            var optionDialogResult = _dialogService.OpenDialog(new OptionDialogViewModel("Potvrda", "Da li ste sigurni da želite da napravite nalog?"));
            if (optionDialogResult == DialogResults.Yes)
            {
                _clientService.Create(new Client
                {
                    FirstName = FirstName.Trim(),
                    LastName = LastName.Trim(),
                    MailAddress = MailAddress.Trim(),
                    Password = Password.Trim(),
                    UserName = UserName.Trim(),
                    PhoneNumber = PhoneNumber.Trim(),
                    Role = Role.User
                });
                EventBus.FireEvent("BackToLogin");
                _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje", "Uspešno ste napravili nalog."));
            }
        }

        private void UpdateClient()
        {
            Client client = GlobalStore.ReadObject<BaseUser>("loggedUser") as Client;

            if (client.UserName != UserName && _clientService.AlreadyInUse(UserName))
            {
                _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje", "Zadato korisnicko ime je vec iskorisceno"));
                return;
            }
            var optionDialogResult = _dialogService.OpenDialog(new OptionDialogViewModel("Potvrda", "Da li ste sigurni da želite da promenite podatke?"));
            if (optionDialogResult == DialogResults.Yes)
            {
                client.FirstName = FirstName.Trim();
                client.LastName = LastName.Trim();
                client.MailAddress = MailAddress.Trim();
                client.Password = Password.Trim();
                client.UserName = UserName.Trim();
                client.PhoneNumber = PhoneNumber.Trim();
                client.Role = Role.User;
                _clientService.Update(client);
                EventBus.FireEvent("ClientLogin");
            }
        }

        private string Err(string message)
        {
            return _calls++ < 7 ? "*" : message;   // there are 7 fields
        }

        public void ForUpdate()
        {
            ForEdit = true;
            ButtonText = "Sacuvaj";
            _calls = 7;
            if (!(GlobalStore.ReadObject<BaseUser>("loggedUser") is Client client)) return;
            FirstName = client.FirstName;
            LastName = client.LastName;
            UserName = client.UserName;
            MailAddress = client.MailAddress;
            PhoneNumber = client.PhoneNumber;
            Password = client.Password;
            RepeatedPassword = Password;
        }
    }
}
