using Organizator_Proslava.Model.CelebrationResponses;
using System;
using System.Collections.Generic;

namespace Organizator_Proslava.Services.Contracts
{
    public interface ICelebrationResponseService : ICrudService<CelebrationResponse>
    {
        IEnumerable<CelebrationResponse> ReadOrganizingBy(Guid organizerId);
        IEnumerable<CelebrationResponse> ReadForCelebration(Guid celebrationId);
    }
}