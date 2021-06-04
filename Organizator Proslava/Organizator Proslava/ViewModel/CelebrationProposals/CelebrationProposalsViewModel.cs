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
                _cdtvm.CelebrationResponse = _celebrationResponseService.ReadForCelebration(_celebration.Id);
            }
        }

        private readonly ICelebrationResponseService _celebrationResponseService;
        private readonly DatabaseContext _context;

        private readonly CelebrationDetailsTableViewModel _cdtvm;
        private readonly CelebrationProposalsTableViewModel _cptvm;
        
        public CelebrationProposalsViewModel(
            CelebrationDetailsTableViewModel cdtvm,
            CelebrationProposalsTableViewModel cptvm,
            ICelebrationResponseService celebrationResponseService,
            DatabaseContext context)
        {
            Switch(cdtvm);
            
            _celebrationResponseService = celebrationResponseService;
            _context = context;

            _cdtvm = cdtvm;
            _cptvm = cptvm;
            
            EventBus.RegisterHandler("BackToCelebrationDetailsTable", cr =>
            {
                _cdtvm.CelebrationResponse = cr as CelebrationResponse;
                Switch(_cdtvm);
            });
            EventBus.RegisterHandler("SwitchCelebrationProposalsViewModel", vm => Switch(vm));
        }

    }
}