using Organizator_Proslava.Model;
using System;
using System.Collections.Generic;

namespace Organizator_Proslava.Services.Contracts
{
    public interface ICelebrationService : ICrudService<Celebration>
    {
        Celebration AcceptBy(Guid organizerId, Guid celebrationId);

        IEnumerable<Celebration> ReadNotTaken();
        IEnumerable<Celebration> ReadForClient(Guid clientId);
        int GetNumOfDoneCelebrationsForOrganizer(Guid organizerId);
        bool CanDeleteOrganizer(Guid organizerId);
    }
}