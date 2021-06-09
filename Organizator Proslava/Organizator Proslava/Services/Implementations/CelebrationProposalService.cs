using Organizator_Proslava.Data;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Organizator_Proslava.Services.Implementations
{
    public class CelebrationProposalService : CrudService<CelebrationProposal>, ICelebrationProposalService
    {
        public CelebrationProposalService(DatabaseContext context) :
            base(context)
        {
        }

        public IEnumerable<CelebrationProposal> ReadFor(Guid detailId)
        {
            return _entities.Where(proposal => proposal.CelebrationDetailId == detailId).ToList();
        }

        public bool CanDeleteCollaborator(Guid collaboratorId)
        {
            return !_entities.Any(celebrationProposal => celebrationProposal.CollaboratorId == collaboratorId &&
                                                        celebrationProposal.CelebrationResponse.Celebration.DateTimeTo >=
                                                        DateTime.Now);
        }
    }
}