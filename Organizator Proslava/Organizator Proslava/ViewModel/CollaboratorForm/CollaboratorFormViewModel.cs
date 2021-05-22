using Organizator_Proslava.Data;
using Organizator_Proslava.Utility;
using System.Diagnostics;
using System.Linq;

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

        private string _selectedCollaboratorType;
        private DatabaseContext _context;

        public CollaboratorFormViewModel(
            SelectCollaboratorTypeViewModal sctvm,
            IndividualCollaboratorInformationsViewModel icivm,
            LegalCollaboratorInformationsViewModel lcivm,
            CollaboratorServicesViewModel csvm,
            CollaboratorImagesViewModel civm,
            CollaboratorHallsViewModel chvm,
            DatabaseContext context)
        {
            Sctvm = sctvm;
            Icivm = icivm;
            Lcivm = lcivm;
            Csvm = csvm;
            Civm = civm;
            Chvm = chvm;
            _context = context;

            Switch(sctvm);

            RegisterHandlerToEventBus();
        }

        private void RegisterHandlerToEventBus()
        {
            EventBus.RegisterHandler("BackToSelectCollaboratorType", () => Switch(Sctvm));
            EventBus.RegisterHandler("IndividualSelected", () => Switch(Icivm));
            EventBus.RegisterHandler("LegalSelected", () => Switch(Lcivm));
            EventBus.RegisterHandler("NextToCollaboratorServicesFromLegal", () => { _selectedCollaboratorType = "Legal"; Csvm.CameFrom = "Legal"; Switch(Csvm); });
            EventBus.RegisterHandler("NextToCollaboratorServicesFromIndividual", () => { _selectedCollaboratorType = "Individual"; Csvm.CameFrom = "Individual"; Switch(Csvm); });
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
            EventBus.RegisterHandler("SaveCollaborator", SaveCollaborator);
        }

        private void SaveCollaborator()
        {
            var collaborator = _selectedCollaboratorType == "Legal" ? Lcivm.Collaborator : Icivm.Collaborator;
            collaborator.CollaboratorServiceBook = Csvm.CollaboratorServiceBook;
            collaborator.Images = Civm.Images.ToList();
            collaborator.CelebrationHalls = Chvm.CelebrationHalls;

            _context.Add(collaborator);
            _context.SaveChanges();
            EventBus.FireEvent("BackToCollaboratorsTable");
        }
    }
}