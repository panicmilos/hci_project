using Organizator_Proslava.Model;
using Organizator_Proslava.Ninject;
using Organizator_Proslava.Services.Contracts;
using System.Collections.ObjectModel;

namespace Organizator_Proslava.UserCommands
{
    public class DeleteClientUserCommands : IUserCommand
    {
        private readonly Client _client;
        private readonly ObservableCollection<Client> _clients;

        private readonly IClientService _clientService;

        public DeleteClientUserCommands(Client client, ObservableCollection<Client> Clients)
        {
            _client = client;
            _clients = Clients;
            _clientService = ServiceLocator.Get<IClientService>();
        }

        public void Redo()
        {
            _clientService.Delete(_client.Id);
            _clients.Remove(_client);
        }

        public void Undo()
        {
            _clientService.Create(_client);
            _clients.Add(_client);
        }
    }
}