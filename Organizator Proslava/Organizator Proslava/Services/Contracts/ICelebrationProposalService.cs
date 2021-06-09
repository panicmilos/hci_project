using Organizator_Proslava.Model.CelebrationResponses;
using System;
using System.Collections.Generic;

namespace Organizator_Proslava.Services.Contracts
{
    public interface ICelebrationProposalService : ICrudService<CelebrationProposal>
    {
        IEnumerable<CelebrationProposal> ReadFor(Guid detailId);
    }
}