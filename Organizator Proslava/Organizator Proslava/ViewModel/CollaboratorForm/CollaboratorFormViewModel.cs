using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel.CollaboratorForm
{
    public class CollaboratorFormViewModel : NavigabileModelView
    {
        public SelectCollaboratorTypeViewModal Sctvm { get; set; }
        public IndividualCollaboratorInformationsViewModel Icivm { get; set; }
        public LegalCollaboratorInformationsViewModel Lcivm { get; set; }

        public CollaboratorFormViewModel(
            SelectCollaboratorTypeViewModal sctvm,
            IndividualCollaboratorInformationsViewModel icivm,
            LegalCollaboratorInformationsViewModel lcivm)
        {
            Sctvm = sctvm;
            Icivm = icivm;
            Lcivm = lcivm;
            Switch(sctvm);

            RegisterHandlerToEventBus();
        }

        private void RegisterHandlerToEventBus()
        {
            EventBus.RegisterHandler("IndividualSelected", () => Switch(Icivm));
            EventBus.RegisterHandler("LegalSelected", () => Switch(Lcivm));
        }
    }
}