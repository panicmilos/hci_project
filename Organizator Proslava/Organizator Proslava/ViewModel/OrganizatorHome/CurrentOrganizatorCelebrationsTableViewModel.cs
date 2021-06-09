using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Custom.Notifications;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.UserCommands;
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
        public ICommand Logout { get; set; }
        public ICommand Notifications { get; set; }

        public ICommand Preview { get; set; }
        public ICommand Cancel { get; set; }

        private readonly ICelebrationResponseService _celebrationResponseService;
        private readonly IDialogService _dialogService;
        private readonly INotificationService _notificationService;

        private readonly CelebrationResponseFormViewModel _crfvm;
        private readonly CollaboratorsTableViewModel _ctvm;

        public CurrentOrganizatorCelebrationsTableViewModel(
            CelebrationResponseFormViewModel crfvm,
            CollaboratorsTableViewModel ctvm,
            ICelebrationResponseService celebrationResponseService,
            IDialogService dialogService,
            INotificationService notificationService)
        {
            _celebrationResponseService = celebrationResponseService;
            _dialogService = dialogService;
            _notificationService = notificationService;

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

            Cancel = new RelayCommand<CelebrationResponse>(cr =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Potvrda otkazivanja proslave",
                        "Da li ste sigurni da želite da otkažete organizovanje proslave?")) == DialogResults.Yes)
                {
                    var createdCanceledNotification = _notificationService.Create(new CanceledResponseNotification
                    {
                        ForUserId = cr.Celebration.ClientId.Value,
                        Organizer = cr.Celebration.Organizer.FullName,
                        CelebrationType = cr.Celebration.Type
                    });

                    var notifications = _notificationService.ReadFrom(cr.Id);
                    GlobalStore.ReadObject<IUserCommandManager>("userCommands").Add(
                        new CancelCelebrationResponse(cr, createdCanceledNotification, notifications));

                    CelebrationResponses.Remove(cr);
                    _celebrationResponseService.CancelCelebrationResponse(cr.Id);
                }
            });

            Logout = new RelayCommand(() =>
            {
                GlobalStore.ReadObject<IUserCommandManager>("userCommands").Clear();
                GlobalStore.RemoveObject("loggedUser");
                EventBus.FireEvent("BackToLogin");
            });

            Notifications = new RelayCommand(() =>
            {
                _dialogService.OpenDialog(new NotificationsDialogViewModel(_notificationService));
            });

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