using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using System.Linq;

namespace Organizator_Proslava.ViewModel.CelebrationResponseForm
{
    public class CelebrationResponseFormViewModel : NavigabileModelView

    {
        private readonly RequestLongPreviewViewModel _rlpvm;
        private readonly RequestDetailsTableForOrganizerViewModel _rdtfovm;

        public CelebrationResponseFormViewModel(
            RequestLongPreviewViewModel rlpvm,
            RequestDetailsTableForOrganizerViewModel rdtfovm)
        {
            _rlpvm = rlpvm;
            _rdtfovm = rdtfovm;

            Switch(_rlpvm);

            RegisterHandlersToEventBus();
        }

        private void RegisterHandlersToEventBus()
        {
            EventBus.RegisterHandler("ShowCelebrationRequest", c => ShowCelebrationRequest(c as Celebration));
            EventBus.RegisterHandler("NextToRequestDetailsForOrganizer", () => Switch(_rdtfovm));
            EventBus.RegisterHandler("BackToRequestLongPreviewForOranizer", () => Switch(_rlpvm));
        }

        private void ShowCelebrationRequest(Celebration celebration)
        {
            _rlpvm.Celebration = celebration;
            _rdtfovm.CelebrationDetails = celebration.CelebrationDetails.ToList();
            Switch(_rlpvm);
        }
    }
}