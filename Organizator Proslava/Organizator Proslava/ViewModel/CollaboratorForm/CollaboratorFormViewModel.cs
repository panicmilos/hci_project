using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.UserCommands;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.IO;
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

        private readonly ICollaboratorService _collaboratorService;

        public CollaboratorFormViewModel(
            SelectCollaboratorTypeViewModal sctvm,
            IndividualCollaboratorInformationsViewModel icivm,
            LegalCollaboratorInformationsViewModel lcivm,
            CollaboratorServicesViewModel csvm,
            CollaboratorImagesViewModel civm,
            CollaboratorHallsViewModel chvm,
            ICollaboratorService collaboratorService)
        {
            Sctvm = sctvm;
            Icivm = icivm;
            Lcivm = lcivm;
            Csvm = csvm;
            Civm = civm;
            Chvm = chvm;
            _collaboratorService = collaboratorService;

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

            EventBus.RegisterHandler("CollaboratorFormForAdd", ForAdd);
            EventBus.RegisterHandler("CollaboratorFormForUpdate", collaborator => ForUpdate(collaborator));
            EventBus.RegisterHandler("AddCollaborator", AddCollaborator);
            EventBus.RegisterHandler("UpdateCollaborator", UpdateCollaborator);
        }

        public void ForAdd()
        {
            Switch(Sctvm);
            Lcivm.ForAdd();
            Icivm.ForAdd();
            Csvm.ForAdd();
            Civm.ForAdd();
            Chvm.ForAdd();
        }

        public void ForUpdate(object collaboratorObject)
        {
            var collaborator = collaboratorObject as Collaborator;

            var collaboratorCopy = collaborator.Clone();

            if (collaboratorCopy is LegalCollaborator)
            {
                Switch(Lcivm);
            }
            else
            {
                Switch(Icivm);
            }

            Lcivm.ForUpdate(collaboratorCopy);
            Icivm.ForUpdate(collaboratorCopy);
            Csvm.ForUpdate(collaboratorCopy);
            Civm.ForUpdate(collaboratorCopy);
            Chvm.ForUpdate(collaboratorCopy);
        }

        private void AddCollaborator()
        {
            var collaborator = CollectCollaboratorInformations();
            _collaboratorService.Create(collaborator);
            EventBus.FireEvent("ReloadCollaboratorTable");
            EventBus.FireEvent("BackToCollaboratorsTable");

            GlobalStore.ReadObject<IUserCommandManager>("userCommands").Clear();
            GlobalStore.ReadObject<IUserCommandManager>("userCommands").Add(new CreateCollaborator(collaborator));
        }

        private void UpdateCollaborator()
        {
            var collaborator = CollectCollaboratorInformations();

            var currentCollaboratorCopy = _collaboratorService.Read(collaborator.Id).Clone();
            var newCollaboratorCopy = collaborator.Clone();
            GlobalStore.ReadObject<IUserCommandManager>("userCommands").Clear();
            GlobalStore.ReadObject<IUserCommandManager>("userCommands").Add(new UpdateCollaborator(currentCollaboratorCopy, newCollaboratorCopy));

            _collaboratorService.Update(collaborator);
            EventBus.FireEvent("ReloadCollaboratorTable");
            EventBus.FireEvent("BackToCollaboratorsTable");
        }

        private Collaborator CollectCollaboratorInformations()
        {
            var collaborator = _selectedCollaboratorType == "Legal" ? Lcivm.CollectCollaborator() : Icivm.CollectCollaborator();
            collaborator.CollaboratorServiceBook = Csvm.CollaboratorServiceBook;
            collaborator.CelebrationHalls = Chvm.CelebrationHalls;

            var newImagePaths = new List<string>();
            foreach (var imagePath in Civm.Images.ToList())
            {
                if (!Directory.Exists(".\\images"))
                {
                    Directory.CreateDirectory(".\\images");
                }
                if (!imagePath.StartsWith("pack://siteoforigin:,,,"))
                {
                    var newImagePath = "./images/" + Guid.NewGuid() + Path.GetExtension(imagePath);
                    File.Copy(imagePath, newImagePath);
                    newImagePaths.Add("pack://siteoforigin:,,," + newImagePath.Substring(1));
                }
                else
                {
                    newImagePaths.Add(imagePath);
                }
            }
            collaborator.Images = newImagePaths;

            return collaborator;
        }
    }
}