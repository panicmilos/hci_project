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
            existingOrganizer.CellebrationType = organizer.CellebrationType;
            existingOrganizer.JMBG = organizer.JMBG;

            return base.Update(existingOrganizer);
        }
    }
}
