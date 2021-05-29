using Organizator_Proslava.Data;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;

namespace Organizator_Proslava.Services.Implementations
{
    public class ProposalCommentService : CrudService<ProposalComment>, IProposalCommentService
    {
        public ProposalCommentService(DatabaseContext context) :
            base(context)
        {
        }
    }
}