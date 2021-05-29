using Organizator_Proslava.Data;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Organizator_Proslava.Services.Implementations
{
    public class CelebrationService : CrudService<Celebration>, ICelebrationService
    {
        public CelebrationService(DatabaseContext context) :
            base(context)
        {
        }

        public IEnumerable<Celebration> ReadNotTaken()
        {
            return _entities.Where(c => c.OrganizerId == null);
        }
    }
}