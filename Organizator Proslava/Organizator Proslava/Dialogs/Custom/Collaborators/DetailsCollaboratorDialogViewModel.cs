using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Utility;
using System.Collections.ObjectModel;
using System.Linq;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    public class DetailsCollaboratorDialogViewModel : NavigableDialogViewModelBase<DialogResults>
    {
        private const string ImagePlaceholderFileName = @"pack://siteoforigin:,,,,/Resources/placeholder.png";
        public Collaborator Collaborator { get; set; }
        public LegalCollaboratorDetailViewModel LegalCollaboratorDetailViewModel { get; set; }
        public IndividualCollaboratorDetailViewModel IndividualCollaboratorDetailViewModel { get; set; }
        public DisplayImagesViewModel DisplayImagesViewModel { get; set; }
        public CollaboratorServiceTableViewModel CollaboratorServiceTableViewModel { get; set; }
        public DisplayHallsDialogViewModel DisplayHallsDialogViewModel { get; set; }
        public SpacePreviewDialogViewModel SpacePreviewDialogViewModel { get; set; }

        public DetailsCollaboratorDialogViewModel(LegalCollaboratorDetailViewModel legalCollaboratorDetailViewModel,
            IndividualCollaboratorDetailViewModel individualCollaboratorDetailViewModel,
            DisplayImagesViewModel displayImagesViewModel,
            CollaboratorServiceTableViewModel collaboratorServiceTableViewModel,
            DisplayHallsDialogViewModel displayHallsDialogViewModel) :
            base("Pregled saradnika", 650, 550)
        {
            LegalCollaboratorDetailViewModel = legalCollaboratorDetailViewModel;
            IndividualCollaboratorDetailViewModel = individualCollaboratorDetailViewModel;
            DisplayImagesViewModel = displayImagesViewModel;
            CollaboratorServiceTableViewModel = collaboratorServiceTableViewModel;
            DisplayHallsDialogViewModel = displayHallsDialogViewModel;

            DisplayInfoAboutCollaborator();

            RegisterHandlerToEventBus();
        }

        public void RegisterHandlerToEventBus()
        {
            EventBus.RegisterHandler("DisplayImages", () =>
            {
                DisplayImagesViewModel.Collaborator = Collaborator;
                DisplayImagesViewModel.Images = new ObservableCollection<string>(Collaborator.Images);
                DisplayImagesViewModel.MainImage = DisplayImagesViewModel.Images.Any() ? Collaborator.Images[0] : ImagePlaceholderFileName;
                Switch(DisplayImagesViewModel);
            });

            EventBus.RegisterHandler("BackToInformations", () =>
            {
                DisplayInfoAboutCollaborator();
            });

            EventBus.RegisterHandler("DisplayHalls", () =>
            {
                DisplayHallsDialogViewModel.Collaborator = Collaborator;
                DisplayHallsDialogViewModel.Halls = new ObservableCollection<CelebrationHall>(Collaborator.CelebrationHalls);
                Switch(DisplayHallsDialogViewModel);
            });

            EventBus.RegisterHandler("DisplayServices", () =>
            {
                CollaboratorServiceTableViewModel.Collaborator = Collaborator;
                CollaboratorServiceTableViewModel.Services = new ObservableCollection<CollaboratorService>(Collaborator.CollaboratorServiceBook.Services);
                Switch(CollaboratorServiceTableViewModel);
            });

            EventBus.RegisterHandler("DisplayOneHall", hall =>
            {
                SpacePreviewDialogViewModel = new SpacePreviewDialogViewModel(hall as CelebrationHall, new DialogService());
                SpacePreviewDialogViewModel.ChangeBack();
                Switch(SpacePreviewDialogViewModel);
            });

            EventBus.RegisterHandler("BackToHallsTable", () =>
            {
                DisplayHallsDialogViewModel.Collaborator = Collaborator;
                DisplayHallsDialogViewModel.Halls = new ObservableCollection<CelebrationHall>(Collaborator.CelebrationHalls);
                Switch(DisplayHallsDialogViewModel);
            });
        }

        public void DisplayInfoAboutCollaborator()
        {
            if (Collaborator is IndividualCollaborator)
            {
                IndividualCollaboratorDetailViewModel.Collaborator = Collaborator as IndividualCollaborator;
                Switch(IndividualCollaboratorDetailViewModel);
            }
            else
            {
                LegalCollaboratorDetailViewModel.Collaborator = Collaborator as LegalCollaborator;
                Switch(LegalCollaboratorDetailViewModel);
            }
        }
    }
}