using Organizator_Proslava.Data;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Organizator_Proslava.Services.Implementations
{
    public class CelebrationResponseService : CrudService<CelebrationResponse>, ICelebrationResponseService
    {
        public CelebrationResponseService(DatabaseContext context) :
            base(context)
        {
        }

        public IEnumerable<CelebrationResponse> ReadOrganizingBy(Guid organizerId)
        {
            return _entities.Where(cr => cr.OrganizerId == organizerId).ToList();
        }

        public CelebrationResponse ReadForCelebration(Guid celebrationId)
        {
            return Read().FirstOrDefault(celebrationResponse => celebrationResponse.CelebrationId == celebrationId);
        }
    }
}