using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Notifications
{
    public class NotificationsDialogViewModel : DialogViewModelBase<DialogResults>
    {
        public ObservableCollection<Notification> Notifications { get; set; }

        public ICommand Delete { get; set; }
        public ICommand Close { get; set; }

        private readonly INotificationService _notificationService;

        public NotificationsDialogViewModel(INotificationService notificationService) :
            base("Obaveštenja", 560, 460)
        {
            _notificationService = notificationService;

            var loggedUser = GlobalStore.ReadObject<BaseUser>("loggedUser").Id;
            Notifications = new ObservableCollection<Notification>(_notificationService.ReadFor(loggedUser));

            Delete = new RelayCommand<Notification>(n => { Notifications.Remove(n); _notificationService.Delete(n.Id); });

            Close = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, DialogResults.Undefined));
        }
    }
}