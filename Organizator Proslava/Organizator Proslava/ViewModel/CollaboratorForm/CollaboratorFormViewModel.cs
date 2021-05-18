using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel.CollaboratorForm
{
    public class CollaboratorFormViewModel : NavigabileModelView
    {
        public SelectCollaboratorTypeViewModal Sctvm { get; set; }
        public IndividualCollaboratorInformationsViewModel Icivm { get; set; }
        public LegalCollaboratorInformationsViewModel Lcivm { get; set; }
        public CollaboratorServicesViewModel Csvm { get; set; }

        public CollaboratorFormViewModel(
            SelectCollaboratorTypeViewModal sctvm,
            IndividualCollaboratorInformationsViewModel icivm,
            LegalCollaboratorInformationsViewModel lcivm,
            CollaboratorServicesViewModel csvm)
        {
            Sctvm = sctvm;
            Icivm = icivm;
            Lcivm = lcivm;
            Csvm = csvm;
            Switch(sctvm);

            RegisterHandlerToEventBus();
        }

        private void RegisterHandlerToEventBus()
        {
            EventBus.RegisterHandler("BackToSelectCollaboratorType", () => Switch(Sctvm));
            EventBus.RegisterHandler("IndividualSelected", () => Switch(Icivm));
            EventBus.RegisterHandler("LegalSelected", () => Switch(Lcivm));
            EventBus.RegisterHandler("NextToCollaboratorServicesFromLegal", () => { Csvm.CameFrom = "Legal"; Switch(Csvm); });
            EventBus.RegisterHandler("NextToCollaboratorServicesFromIndividual", () => { Csvm.CameFrom = "Individual"; Switch(Csvm); });
            EventBus.RegisterHandler("BackToCollaboratorInformations", type =>
            {
                if (type.ToString() == "Legal")
                {
                    Switch(Lcivm);
                }
                else
                {
                    Switch(Icivm);
                }
            });
        }
    }
}