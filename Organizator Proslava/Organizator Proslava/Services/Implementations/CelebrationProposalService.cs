using Organizator_Proslava.Data;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;

namespace Organizator_Proslava.Services.Implementations
{
    public class CelebrationProposalService : CrudService<CelebrationProposal>, ICelebrationProposalService
    {
        public CelebrationProposalService(DatabaseContext context) :
            base(context)
        {
        }
    }
}