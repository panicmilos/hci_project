using Organizator_Proslava.Data;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Organizator_Proslava.Services.Implementations
{
    public class CelebrationService : CrudService<Celebration>, ICelebrationService
    {
        private readonly IOrganizerService _organizerService;

        public CelebrationService(IOrganizerService organizerService, DatabaseContext context) :
            base(context)
        {
            _organizerService = organizerService;
        }

        public Celebration AcceptBy(Guid organizerId, Guid celebrationId)
        {
            var organizer = _organizerService.Read(organizerId);
            var celebration = Read(celebrationId);

            celebration.OrganizerId = organizer.Id;

            return base.Update(celebration);
        }

        public IEnumerable<Celebration> ReadNotTaken()
        {
            return _entities.Where(c => c.OrganizerId == null);
        }
    }
}