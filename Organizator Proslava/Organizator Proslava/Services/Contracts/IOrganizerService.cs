using Organizator_Proslava.Model;
using System.Collections.Generic;

namespace Organizator_Proslava.Services.Contracts
{
    public interface IOrganizerService : IUserService<Organizer>
    {
        IEnumerable<Organizer> ReadSpecifiedFor(string celebrationTypeName);

        bool OrganizersExistFor(string celebrationTypeName);
    }
}