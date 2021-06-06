using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Alert;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.Utils;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class RegisterViewModel : BaseObservableEntity, IDataErrorInfo
    {
        // Text fields:
        public string FirstName { get; set; }

        public string LastName { get; set; }
        public string MailAddress { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }

        private string _password;
        public string Password { get { return _password; } set { OnPropertyChanged(ref _password, value); OnPropertyChanged("RepeatedPassword"); } }

        private string _repeatedPassword;
        public string RepeatedPassword { get { return _repeatedPassword; } set { OnPropertyChanged(ref _repeatedPassword, value); OnPropertyChanged("Password"); } }

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
                if (columnName == "Password" || columnName == "RepeatedPassword")
                {
                    return Err(ValidationDictionary.Validate(columnName, Password, RepeatedPassword));
                }

                var valueOfProperty = GetType().GetProperty(columnName)?.GetValue(this);
                return Err(ValidationDictionary.Validate(columnName, valueOfProperty, null));
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
            if (message == null)
            {
                return null;
            }

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