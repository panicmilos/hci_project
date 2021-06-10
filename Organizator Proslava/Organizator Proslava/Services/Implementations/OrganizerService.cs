using Microsoft.EntityFrameworkCore;
using Organizator_Proslava.Data;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using System.Collections.Generic;
using System.Linq;

namespace Organizator_Proslava.Services.Implementations
{
    public class OrganizerService : UserService<Organizer>, IOrganizerService
    {
        public OrganizerService(DatabaseContext context) :
            base(context)
        {
        }

        public override Organizer Update(Organizer organizer)
        {
            var existingOrganizer = Read(organizer.Id);

            existingOrganizer.FirstName = organizer.FirstName;
            existingOrganizer.LastName = organizer.LastName;
            existingOrganizer.PhoneNumber = organizer.PhoneNumber;
            existingOrganizer.MailAddress = organizer.MailAddress;

            existingOrganizer.Address.Lat = organizer.Address.Lat;
            existingOrganizer.Address.Lng = organizer.Address.Lng;
            existingOrganizer.Address.WholeAddress = organizer.Address.WholeAddress;
            existingOrganizer.PersonalId = organizer.PersonalId;

            var oldCelebrationType = existingOrganizer.CellebrationType;
            existingOrganizer.CellebrationType = organizer.CellebrationType;
            existingOrganizer.JMBG = organizer.JMBG;

            existingOrganizer.UserName = organizer.UserName;
            existingOrganizer.Password = organizer.Password;

            var updatedOrganizer = base.Update(existingOrganizer);
            _context.Entry(organizer.CellebrationType).State = EntityState.Detached;
            _context.Entry(oldCelebrationType).State = EntityState.Detached;

            return updatedOrganizer;
        }

        public bool OrganizersExistFor(string celebrationTypeName)
        {
            return _entities.Any(organizer => organizer.CellebrationType.Name == celebrationTypeName);
        }

        public IEnumerable<Organizer> ReadSpecifiedFor(string celebrationTypeName)
        {
            return _entities.Where(organizer => organizer.CellebrationType.Name == celebrationTypeName).ToList();
        }
    }
}