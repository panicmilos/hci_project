using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Alert;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.Utils;
using System.ComponentModel;
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
        public string Password { get => _password; set { OnPropertyChanged(ref _password, value); OnPropertyChanged("RepeatedPassword"); } }
        public string RepeatedPassword { get => _repeatedPassword; set { OnPropertyChanged(ref _repeatedPassword, value); OnPropertyChanged("Password"); } }

        private string _password, _repeatedPassword;

        // Commands:
        public ICommand Register { get; set; }
        public ICommand Back { get; set; }

        public bool ForEdit { get; set; } = false;
        public string ButtonText { get; set; } = "Napravi nalog";

        // Rules:
        public string Error => throw new System.NotImplementedException();
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Password" || columnName == "RepeatedPassword")
                    return Err(ValidationDictionary.Validate(columnName, Password, RepeatedPassword));

                var valueOfProperty = GetType().GetProperty(columnName)?.GetValue(this);
                return Err(ValidationDictionary.Validate(columnName, valueOfProperty, null));
            }
        }

        private readonly IClientService _clientService;
        private readonly IDialogService _dialogService;
        private int _calls = 0;

        public RegisterViewModel(IClientService clientService, IDialogService dialogService)
        {
            _clientService = clientService;
            _dialogService = dialogService;

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
                _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje", "Zadato korisničko ime je već iskorišćeno."));
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
            return message == null ? null : (_calls++ < 7 ? "*" : message);   // there are 7 fields
        }

        public void ForUpdate()
        {
            ForEdit = true;
            ButtonText = "Sačuvaj";
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
