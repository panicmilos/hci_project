using Organizator_Proslava.Data;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizator_Proslava.Services.Implementations
{
    public class OrganizerService : UserService<Organizer>, IOrganizerService
    {
        public OrganizerService(DatabaseContext context):
            base(context)
        {

        }
    }
}
