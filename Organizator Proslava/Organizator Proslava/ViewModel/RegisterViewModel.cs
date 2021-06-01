using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Alert;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System.ComponentModel;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class RegisterViewModel : IDataErrorInfo
    {
        public Client Client { get; set; }
        public string RepeatedPassword { get; set; }
        public string UN { get; set; }
        public ICommand Register { get; set; }
        public ICommand Back { get; set; }
        public string Error { get; set; }
        public string this[string columnName]
        {
            get
            {
                if (columnName == "RepeatedPassword")
                {
                    if (Client.Password != RepeatedPassword)
                        return "pw and repeated not equal";
                }
                else if (columnName == "UN")
                {
                    if (UN.Length < 5) return "username short";
                    if (UN.Length > 15) return "username long";
                }
                return null;
            }
        }

        private readonly IClientService _clientService;
        private readonly IDialogService _dialogService;

        public RegisterViewModel(IClientService clientService, IDialogService dialogService)
        {
            Client = new Client();
            _clientService = clientService;
            _dialogService = dialogService;

            Register = new RelayCommand<Client>(c =>
            {
                var optionDialogResult = _dialogService.OpenDialog(new OptionDialogViewModel("Potvrda", "Da li ste sigurni da želite da napravite nalog?"));
                if (optionDialogResult == DialogResults.Yes)
                {
                    c.Role = Role.User;
                    _clientService.Create(c);
                    EventBus.FireEvent("BackToLogin");
                    _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje", "Uspešno ste napravili nalog."));
                }
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToLogin"));
        }
    }
}