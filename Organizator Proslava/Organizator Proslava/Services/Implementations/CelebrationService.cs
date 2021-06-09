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
        private readonly ICollaboratorService _collaboratorService;

        public CelebrationService(IOrganizerService organizerService,
            ICollaboratorService collaboratorService,
            DatabaseContext context) :
            base(context)
        {
            _organizerService = organizerService;
            _collaboratorService = collaboratorService;
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

        public IEnumerable<Celebration> ReadForClient(Guid clientId)
        {
            return _entities.Where(celebration => celebration.ClientId == clientId).ToList();
        }

        public int GetNumOfDoneCelebrationsForOrganizer(Guid organizerId)
        {
            return _entities
                .Count(celebration => celebration.OrganizerId == organizerId && celebration.DateTimeTo < DateTime.Now);
        }

        public bool CanDeleteOrganizer(Guid organizerId)
        {
            return !_entities.Any(celebration =>
                celebration.OrganizerId == organizerId && celebration.DateTimeTo >= DateTime.Now);
        }
    }
}