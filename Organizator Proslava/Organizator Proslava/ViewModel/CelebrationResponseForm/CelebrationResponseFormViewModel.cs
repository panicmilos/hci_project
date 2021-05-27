using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Utility;

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
            EventBus.RegisterHandler("SwitchCelebrationResponseFormViewModel", vm => Switch(vm));

            EventBus.RegisterHandler("ShowCelebrationRequest", cr => ShowCelebrationRequest(cr as CelebrationResponse));
            EventBus.RegisterHandler("NextToRequestDetailsForOrganizer", () => Switch(_rdtfovm));
            EventBus.RegisterHandler("BackToRequestLongPreviewForOranizer", () => Switch(_rlpvm));
            EventBus.RegisterHandler("BackToRequestDetailsForOrganizer", () => Switch(_rdtfovm));
        }

        private void ShowCelebrationRequest(CelebrationResponse celebrationResponse)
        {
            _rlpvm.CelebrationResponse = celebrationResponse;
            _rdtfovm.CelebrationResponse = celebrationResponse;
            Switch(_rlpvm);
        }
    }
}