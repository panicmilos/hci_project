using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.OrganizatorHome;

namespace Organizator_Proslava.ViewModel
{
    public class OrganizerHomeViewModel : NavigabileModelView
    {
        private readonly CurrentOrganizatorCelebrationsTableViewModel _coctvm;
        private readonly AcceptCelebrationRequestTableViewModel _acrtvm;
        private readonly OrganizersPastCelebrationsTableViewModel _opctvm;

        public OrganizerHomeViewModel(CurrentOrganizatorCelebrationsTableViewModel coctvm, 
                                    AcceptCelebrationRequestTableViewModel acrtvm, 
                                    OrganizersPastCelebrationsTableViewModel opctvm)
        {
            _coctvm = coctvm;
            _acrtvm = acrtvm;
            _opctvm = opctvm;

            EventBus.RegisterHandler("SwitchOrganizerViewModel", vm => Switch(vm));
            EventBus.RegisterHandler("BackToCurrentCelebrationsForOrganizer", () => { _coctvm.Reload(); Switch(_coctvm); });
            EventBus.RegisterHandler("NextToAcceptCelebrationRequestTable", () => { _acrtvm.Reload(); Switch(_acrtvm); });
            EventBus.RegisterHandler("NextToOrganizersPastCelebrations", () => { _opctvm.Reload(); Switch(_opctvm); });

            Switch(_coctvm);
        }
    }
}