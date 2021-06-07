using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Custom.Collaborators;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CelebrationResponseForm
{
    public class ProposalCommentsViewModel : ObservableEntity
    {
        private bool _isForClient = GlobalStore.ReadObject<BaseUser>("loggedUser").Role == Role.User;

        public CelebrationProposal CelebrationProposal { get; set; }
        public ObservableCollection<ProposalComment> ProposalComments { get; set; }

        public ICommand Preview { get; set; }

        public ICommand Back { get; set; }
        public ICommand Comment { get; set; }

        private readonly IProposalCommentService _proposalCommentService;
        private readonly ICelebrationHallService _celebrationHallService;
        private readonly INotificationService _notificationService;
        private readonly IDialogService _dialogService;

        public ProposalCommentsViewModel(
            IProposalCommentService proposalCommentService,
            ICelebrationHallService celebrationHallService,
            INotificationService notificationService,
            IDialogService dialogService)
        {
            _proposalCommentService = proposalCommentService;
            _celebrationHallService = celebrationHallService;
            _notificationService = notificationService;
            _dialogService = dialogService;

            Preview = new RelayCommand(() =>
            {
                var celebrationHallCopy = CelebrationProposal.CelebrationHall.Clone();

                var editedCelebrationHall = _dialogService.OpenDialog(new SpacePreviewDialogViewModel(new SpacePreviewViewModel(celebrationHallCopy, _isForClient ? SpacePreviewMode.Edit : SpacePreviewMode.View), _dialogService));
                if (editedCelebrationHall != null)
                {
                    CelebrationProposal.CelebrationHall.PlaceableEntities = editedCelebrationHall.PlaceableEntities;
                    _celebrationHallService.Update(CelebrationProposal.CelebrationHall);
                }
            });

            Back = new RelayCommand(() => EventBus.FireEvent(_isForClient ? "BackToProposalsTableForClient" : "BackToProposalsTableForOrganizer"));

            Comment = new RelayCommand(() =>
            {
                var commentText = _dialogService.OpenDialog(new WriteCommentDialogViewModel(_dialogService));
                if (commentText != null)
                {
                    var loggedUserId = GlobalStore.ReadObject<BaseUser>("loggedUser").Id;
                    var comment = new ProposalComment()
                    {
                        WriterId = loggedUserId,
                        CelebrationProposalId = CelebrationProposal.Id,
                        Content = commentText
                    };
                    _proposalCommentService.Create(comment);

                    var forUserId = loggedUserId == CelebrationProposal.CelebrationResponse.Celebration.Client.Id ? CelebrationProposal.CelebrationResponse.Celebration.Organizer.Id : CelebrationProposal.CelebrationResponse.Celebration.Client.Id;
                    _notificationService.Create(new NewCommentNotification
                    {
                        ForUserId = forUserId,
                        ProposalId = CelebrationProposal.Id,
                        CelebrationResponseId = CelebrationProposal.CelebrationResponseId,
                        NumOfComments = 1
                    });

                    ProposalComments.Add(comment);
                }
            });
        }

        public void ChangeBack()
        {
            Back = new RelayCommand(() => EventBus.FireEvent("BackToProposalsTableForAdmin"));
        }
    }
}