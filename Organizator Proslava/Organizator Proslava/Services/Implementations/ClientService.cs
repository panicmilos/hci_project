using Organizator_Proslava.Data;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;

namespace Organizator_Proslava.Services.Implementations
{
    public class ClientService : UserService<Client>, IClientService
    {
        public ClientService(DatabaseContext context) :
            base(context)
        {
        }
    }
}