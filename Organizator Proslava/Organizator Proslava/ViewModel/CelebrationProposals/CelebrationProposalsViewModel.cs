using System.Linq;
using Organizator_Proslava.Data;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel.CelebrationProposals
{
    public class CelebrationProposalsViewModel : NavigabileModelView
    {
        private Celebration _celebration;
        public Celebration Celebration
        {
            get => _celebration;
            set
            {
                _celebration = value;
                _crtvm.Celebration = value;
            }
        }

        private readonly ICelebrationResponseService _celebrationResponseService;
        private readonly DatabaseContext _context;

        private readonly CelebrationResponsesTableViewModel _crtvm;
        private readonly CelebrationDetailsTableViewModel _cdtvm;
        private readonly CelebrationProposalsTableViewModel _cptvm;
        
        public CelebrationProposalsViewModel(
            CelebrationResponsesTableViewModel crtvm,
            CelebrationDetailsTableViewModel cdtvm,
            CelebrationProposalsTableViewModel cptvm,
            ICelebrationResponseService celebrationResponseService,
            DatabaseContext context)
        {
            Switch(crtvm);
            
            _celebrationResponseService = celebrationResponseService;
            _context = context;

            _crtvm = crtvm;
            _cdtvm = cdtvm;
            _cptvm = cptvm;
            
            EventBus.RegisterHandler("BackToCelebrationResponsesTable", () => Switch(_crtvm));
            EventBus.RegisterHandler("BackToCelebrationDetailsTable", cr =>
            {
                _cdtvm.CelebrationResponse = cr as CelebrationResponse;
                Switch(_cdtvm);
            });
            EventBus.RegisterHandler("SwitchCelebrationProposalsViewModel", vm => Switch(vm));
        }

    }
}