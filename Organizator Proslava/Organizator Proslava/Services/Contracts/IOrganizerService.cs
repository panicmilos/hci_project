using Organizator_Proslava.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizator_Proslava.Services.Contracts
{
    public interface IOrganizerService : IUserService<Organizer>
    {
        bool OrganizersExistFor(string celebrationTypeName);
    }
}
