using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CelebrationResponseForm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.OrganizatorHome
{
    public class CurrentOrganizatorCelebrationsTableViewModel
    {
        public ObservableCollection<CelebrationResponse> CelebrationResponses { get; set; }

        public ICommand NonAcceptedCelebrations { get; set; }
        public ICommand Collaborators { get; set; }

        public ICommand Preview { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand Back { get; set; }

        private readonly ICelebrationResponseService _celebrationResponseService;

        private readonly CelebrationResponseFormViewModel _crfvm;
        private readonly CollaboratorsTableViewModel _ctvm;

        public CurrentOrganizatorCelebrationsTableViewModel(
            ICelebrationResponseService celebrationResponseService,
            CelebrationResponseFormViewModel crfvm,
            CollaboratorsTableViewModel ctvm)
        {
            _celebrationResponseService = celebrationResponseService;
            CelebrationResponses = new ObservableCollection<CelebrationResponse>();

            NonAcceptedCelebrations = new RelayCommand(() => EventBus.FireEvent("NextToAcceptCelebrationRequestTable"));

            _crfvm = crfvm;
            _ctvm = ctvm;

            Preview = new RelayCommand<CelebrationResponse>(cr =>
            {
                EventBus.FireEvent("ShowCelebrationRequest", cr);
                EventBus.FireEvent("SwitchOrganizerViewModel", _crfvm);
            });

            Collaborators = new RelayCommand(() =>
            {
                _ctvm.ForOrganizer();
                EventBus.FireEvent("SwitchOrganizerViewModel", _ctvm);
            });
            EventBus.RegisterHandler("BackToCollaboratorsForOrganizer", () => Collaborators.Execute(null));

            EventBus.RegisterHandler("PreviewResponseForNotification", cr => Preview.Execute(cr));

            Cancel = new RelayCommand(() =>
                new DialogService().OpenDialog(new OptionDialogViewModel("Potvrda otkazivanja proslave",
                        "Da li ste sigurni da želite da otkažete organizovanje proslave?")));

            Back = new RelayCommand(() => EventBus.FireEvent("BackToLogin"));

            EventBus.RegisterHandler("ReloadCurrentOrganizatorCelebrationsTable", () => Reload());

            Reload();
        }

        public void Reload()
        {
            var loggedUser = GlobalStore.ReadObject<BaseUser>("loggedUser");
            CelebrationResponses.Clear();
            _celebrationResponseService.ReadOrganizingBy(loggedUser.Id).ToList().ForEach(cr => CelebrationResponses.Add(cr));
        }
    }
}