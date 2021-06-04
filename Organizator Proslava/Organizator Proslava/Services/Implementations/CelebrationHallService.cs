using Organizator_Proslava.Data;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Services.Contracts;

namespace Organizator_Proslava.Services.Implementations
{
    public class CelebrationHallService : CrudService<CelebrationHall>, ICelebrationHallService
    {
        public CelebrationHallService(DatabaseContext context) :
            base(context)
        {
        }
    }
}