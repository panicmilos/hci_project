using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CelebrationResponseForm
{
    public class ProposalCommentsViewModel : ObservableEntity
    {
        public CelebrationProposal CelebrationProposal { get; set; }
        public ObservableCollection<ProposalComment> ProposalComments { get; set; }

        public ICommand Back { get; set; }
        public ICommand Comment { get; set; }

        private readonly IProposalCommentService _proposalCommentService;
        private readonly INotificationService _notificationService;
        private readonly IDialogService _dialogService;

        public ProposalCommentsViewModel(IProposalCommentService proposalCommentService, INotificationService notificationService, IDialogService dialogService)
        {
            _proposalCommentService = proposalCommentService;
            _notificationService = notificationService;
            _dialogService = dialogService;

            Back = new RelayCommand(() => EventBus.FireEvent("BackToProposalsTableForOrganizer"));
            Comment = new RelayCommand(() =>
            {
                var commentText = _dialogService.OpenDialog(new WriteCommentDialogViewModel(_dialogService));
                if (commentText != null)
                {
                    var loggedUser = GlobalStore.ReadObject<BaseUser>("loggedUser");
                    var comment = new ProposalComment()
                    {
                        WriterId = loggedUser.Id,
                        CelebrationProposalId = CelebrationProposal.Id,
                        Content = commentText
                    };

                    _proposalCommentService.Create(comment);
                    _notificationService.Create(new NewCommentNotification
                    {
                        ForUserId = GlobalStore.ReadObject<BaseUser>("loggedUser").Id,
                        ProposalId = CelebrationProposal.Id,
                        CelebrationResponseId = CelebrationProposal.CelebrationResponseId,
                        NumOfComments = 1
                    });

                    ProposalComments.Add(comment);
                }
            });
        }
    }
}