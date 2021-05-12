using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class RegisterViewModel
    {
        public Client Client { get; set; }
        public ICommand Register { get; set; }

        private readonly IClientService _clientService;

        public RegisterViewModel(IClientService clientService)
        {
            Client = new Client();
            _clientService = clientService;
            Register = new RelayCommand<Client>(c =>
            {
                _clientService.Create(c);
                EventBus.FireEvent("BackToLogin");
            });
        }
    }
}