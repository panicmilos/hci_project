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

        public ICommand Preview { get; set; }
        public ICommand Delete { get; set; }

        public ICommand Close { get; set; }

        private readonly INotificationService _notificationService;

        public NotificationsDialogViewModel(INotificationService notificationService) :
            base("Obaveštenja", 560, 460)
        {
            _notificationService = notificationService;

            var loggedUser = GlobalStore.ReadObject<BaseUser>("loggedUser").Id;
            Notifications = new ObservableCollection<Notification>(_notificationService.ReadFor(loggedUser));

            Preview = new RelayCommand<Notification>(n =>
            {
                if (n is NewCommentNotification newComment)
                {
                    // za klijenta
                    EventBus.FireEvent("NextToCelebrationProposals", newComment.CelebrationResponse.Celebration);
                    EventBus.FireEvent("PreviewProposalsFromNotificationClient", newComment.Proposal.CelebrationDetail);
                    EventBus.FireEvent("PreviewCommentsFromNotificationClient", newComment.Proposal);

                    // za organizatora
                    //EventBus.FireEvent("OrganizerLogin");
                    //EventBus.FireEvent("PreviewResponseForNotification", newComment.CelebrationResponse);
                    //EventBus.FireEvent("NextToRequestDetailsForOrganizer");
                    //EventBus.FireEvent("PreviewProposalsFromNotificationOrganizer", newComment.Proposal.CelebrationDetail);
                    //EventBus.FireEvent("PreviewCommentsFromNotificationOrganizer", newComment.Proposal);
                }
            });

            Delete = new RelayCommand<Notification>(n => { Notifications.Remove(n); _notificationService.Delete(n.Id); });

            Close = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, DialogResults.Undefined));
        }
    }
}