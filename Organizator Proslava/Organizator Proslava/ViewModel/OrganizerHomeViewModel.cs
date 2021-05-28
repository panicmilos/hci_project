using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.OrganizatorHome;

namespace Organizator_Proslava.ViewModel
{
    public class OrganizerHomeViewModel : NavigabileModelView
    {
        private readonly CurrentOrganizatorCelebrationsTableViewModel _coctvm;
        private readonly AcceptCelebrationRequestTableViewModel _acrtvm;

        public OrganizerHomeViewModel(CurrentOrganizatorCelebrationsTableViewModel coctvm, AcceptCelebrationRequestTableViewModel acrtvm)
        {
            _coctvm = coctvm;
            _acrtvm = acrtvm;

            EventBus.RegisterHandler("SwitchOrganizerViewModel", vm => Switch(vm));
            EventBus.RegisterHandler("BackToCurrentCelebrationsForOrganizer", () => Switch(_coctvm));
            EventBus.RegisterHandler("NextToAcceptCelebrationRequestTable", () => { _acrtvm.Reload(); Switch(_acrtvm); });

            Switch(_coctvm);
        }
    }
}