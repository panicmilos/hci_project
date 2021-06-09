using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Alert;
using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Custom.Notifications;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.UserCommands;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CelebrationProposals;
using Organizator_Proslava.ViewModel.CelebrationRequestForm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Alert;
using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.ViewModel.CelebrationProposals;
using Organizator_Proslava.UserCommands;
using Organizator_Proslava.Dialogs.Custom.Notifications;
using Organizator_Proslava.ViewModel.ClientHome;

namespace Organizator_Proslava.ViewModel
{
    public class ClientHomeViewModel
    {
        public ICommand Cancel { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Details { get; set; }
        public ICommand Add { get; set; }
        public ICommand PastCelebrations { get; set; }
        public ICommand Notifications { get; set; }
        public ICommand Profile { get; set; }
        public ICommand Logout { get; set; }

        public ObservableCollection<Celebration> Celebrations { get; set; }

        private readonly CelebrationRequestFormViewModel _crfvm;
        private readonly CelebrationProposalsViewModel _cpvm;
        private readonly RegisterViewModel _rvm;
        private readonly ClientsPastCelebrationsTableViewModel _cpctvm;

        private readonly ICelebrationService _celebrationService;
        private readonly IDialogService _dialogService;
        private readonly INotificationService _notificationService;

        public ClientHomeViewModel(
            CelebrationRequestFormViewModel crfvm,
            CelebrationProposalsViewModel cpvm,
            RegisterViewModel rvm,
            ClientsPastCelebrationsTableViewModel cpctvm,
            ICelebrationService celebrationService,
            IDialogService dialogService,
            INotificationService notificationService)
        {
            _crfvm = crfvm;
            _cpvm = cpvm;
            _rvm = rvm;
            _cpctvm = cpctvm;

            _celebrationService = celebrationService;
            _dialogService = dialogService;
            _notificationService = notificationService;

            LoadCelebrations();

            Details = new RelayCommand<Celebration>(celebration =>
            {
                _dialogService.OpenDialog(new CelebrationLongPreviewDialogViewModel(celebration));
            });

            Cancel = new RelayCommand<Celebration>(celebration =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Potvrda",
                    "Da li ste sigurni da želite da otkažete proslavu?")) == DialogResults.No) return;

                if (celebration.Organizer != null)
                {
                    var createdCanceledNotification = _notificationService.Create(new CanceledCelebrationNotification
                    {
                        ForUserId = celebration.Organizer.Id,
                        Client = celebration.Client.FullName,
                        CelebrationType = celebration.Type
                    });
                }

                Celebrations.Remove(Celebrations.FirstOrDefault(c => c.Id == celebration.Id));
                _celebrationService.Delete(celebration.Id);
            });

            Edit = new RelayCommand<Celebration>(celebration =>
            {
                if (celebration.OrganizerId != null)
                {
                    _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje",
                        "Nije moguće menjati informacije o proslavi nakon što je ona preuzeta od strane organizatora."));
                    return;
                }
                EventBus.FireEvent("CelebrationRequestFromForUpdate", celebration);
                EventBus.FireEvent("SwitchMainViewModel", _crfvm);
            });

            Add = new RelayCommand(() =>
            {
                EventBus.FireEvent("CelebrationRequestFormForAdd");
                EventBus.FireEvent("SwitchMainViewModel", _crfvm);
            });

            PastCelebrations = new RelayCommand(() =>
            {
                EventBus.FireEvent("SwitchMainViewModel", _cpctvm);
            });

            Notifications = new RelayCommand(() =>
            {
                _dialogService.OpenDialog(new NotificationsDialogViewModel(_notificationService));
            });

            Profile = new RelayCommand(() =>
            {
                _rvm.ForUpdate();
                EventBus.FireEvent("SwitchMainViewModel", _rvm);
            });

            Logout = new RelayCommand(() =>
            {
                GlobalStore.ReadObject<IUserCommandManager>("userCommands").Clear();
                GlobalStore.RemoveObject("loggedUser");
                EventBus.FireEvent("BackToLogin");
            });

            EventBus.RegisterHandler("NextToCelebrationProposals", celebration =>
            {
                _cpvm.Celebration = celebration as Celebration;
                EventBus.FireEvent("SwitchMainViewModel", _cpvm);
            });
            EventBus.RegisterHandler("CelebrationAddSuccess", LoadCelebrations);
            EventBus.RegisterHandler("CelebrationUpdateSuccess", LoadCelebrations);
        }

        private void LoadCelebrations()
        {
            Celebrations = new ObservableCollection<Celebration>(_celebrationService.ReadFutureForClient(GlobalStore.ReadObject<BaseUser>("loggedUser").Id));
        }
    }
}