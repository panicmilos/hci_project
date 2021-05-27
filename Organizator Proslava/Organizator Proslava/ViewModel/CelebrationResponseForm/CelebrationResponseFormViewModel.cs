using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel.CelebrationResponseForm
{
    public class CelebrationResponseFormViewModel : NavigabileModelView

    {
        private readonly RequestLongPreviewViewModel _rlpvm;

        public CelebrationResponseFormViewModel(RequestLongPreviewViewModel rlpvm)
        {
            _rlpvm = rlpvm;

            Switch(_rlpvm);

            RegisterHandlersToEventBus();
        }

        private void RegisterHandlersToEventBus()
        {
            EventBus.RegisterHandler("ShowCelebrationRequest", c => ShowCelebrationRequest(c as Celebration));
        }

        private void ShowCelebrationRequest(Celebration celebration)
        {
            _rlpvm.Celebration = celebration;
            Switch(_rlpvm);
        }
    }
}