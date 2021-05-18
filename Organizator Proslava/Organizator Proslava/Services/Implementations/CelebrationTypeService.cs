using Organizator_Proslava.Data;
using Organizator_Proslava.Model.Cellebrations;
using Organizator_Proslava.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Organizator_Proslava.Services.Implementations
{
    public class CelebrationTypeService : CrudService<CellebrationType>, ICelebrationTypeService
    {
        public CelebrationTypeService(DatabaseContext context) :
            base(context)
        {
        }

        public IEnumerable<string> ReadNames()
        {
            return Read().Select(celebrationType => celebrationType.Name);
        }
    }
}