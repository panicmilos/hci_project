using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel.CollaboratorForm
{
    public class CollaboratorFormViewModel : NavigabileModelView
    {
        public SelectCollaboratorTypeViewModal Sctvm { get; set; }
        public IndividualCollaboratorInformationsViewModel Icivm { get; set; }
        public LegalCollaboratorInformationsViewModel Lcivm { get; set; }
        public CollaboratorServicesViewModel Csvm { get; set; }
        public CollaboratorImagesViewModel Civm { get; set; }
        public CollaboratorHallsViewModel Chvm { get; set; }

        public CollaboratorFormViewModel(
            SelectCollaboratorTypeViewModal sctvm,
            IndividualCollaboratorInformationsViewModel icivm,
            LegalCollaboratorInformationsViewModel lcivm,
            CollaboratorServicesViewModel csvm,
            CollaboratorImagesViewModel civm,
            CollaboratorHallsViewModel chvm)
        {
            Sctvm = sctvm;
            Icivm = icivm;
            Lcivm = lcivm;
            Csvm = csvm;
            Civm = civm;
            Chvm = chvm;
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
            EventBus.RegisterHandler("BackToCollaboratorServices", () => Switch(Csvm));
            EventBus.RegisterHandler("NextToCollaboratorImages", () => Switch(Civm));
            EventBus.RegisterHandler("BackToCollaboratorImages", () => Switch(Civm));
            EventBus.RegisterHandler("NextToCollaboratorHalls", () => Switch(Chvm));
        }
    }
}