using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Custom.Clients;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.UsersView
{
    public class UsersTableViewModel
    {
        public ObservableCollection<Client> Clients { get; set; }
        public ICommand Back { get; set; }
        public ICommand Remove { get; set; }
        public ICommand Details { get; set; }

        private readonly IClientService _clientService;
        private readonly IDialogService _dialogService;

        public UsersTableViewModel(IClientService clientService, IDialogService dialogService)
        {
            _clientService = clientService;
            _dialogService = dialogService;

            Clients = new ObservableCollection<Client>(_clientService.Read());

            Remove = new RelayCommand<Client>(client =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Pitanje", "Da li ste sigurni da želite da obrišete ovog klijenta?")) == DialogResults.Yes)
                {
                    Clients.Remove(client);
                    _clientService.Delete(client.Id);
                }
            });

            Back = new RelayCommand(() => EventBus.FireEvent("AdminLogin"));

            Details = new RelayCommand<Client>(client =>
            {
                _dialogService.OpenDialog(new ClientDetailViewModel(client));
            });

            EventBus.RegisterHandler("ReloadClientTable", () => Clients = new ObservableCollection<Client>(_clientService.Read()));
        }
    }
}
