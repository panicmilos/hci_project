using Organizator_Proslava.Data;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
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

        public override Celebration Create(Celebration celebration)
        {
            if (celebration.OrganizerId != null)
            {
                _context.Add(new CelebrationResponse
                {
                    Celebration = celebration,
                    OrganizerId = celebration.OrganizerId.Value
                });
                _context.SaveChanges();

                return celebration;
            }

            return base.Create(celebration);
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

        public IEnumerable<Celebration> ReadFutureForClient(Guid clientId)
        {
            return _entities.Where(celebration => celebration.ClientId == clientId && celebration.DateTimeTo >= DateTime.Now).ToList();
        }

        public IEnumerable<Celebration> ReadPastForClient(Guid clientId)
        {
            return _entities.Where(celebration => celebration.ClientId == clientId && celebration.DateTimeTo < DateTime.Now);
        }

        public IEnumerable<Celebration> ReadPastForOrganizer(Guid organizerId)
        {
            return _entities.Where(celebration => celebration.OrganizerId == organizerId && celebration.DateTimeTo < DateTime.Now);
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