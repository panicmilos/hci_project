using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Ninject;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Organizator_Proslava.UserCommands
{
    public class AcceptCelebrationRequest : IUserCommand
    {
        private readonly CelebrationResponse _celebrationResponse;
        private readonly Celebration _celebration;
        private readonly Guid _organizerId;
        private readonly ICelebrationService _celebrationService;
        private readonly ICelebrationResponseService _celebrationResponseService;

        public AcceptCelebrationRequest(CelebrationResponse celebrationResponse)
        {
            _celebrationResponse = celebrationResponse;
            _celebration = celebrationResponse.Celebration;
            _organizerId = celebrationResponse.Celebration.OrganizerId.Value;
            _celebrationService = ServiceLocator.Get<ICelebrationService>();
            _celebrationResponseService = ServiceLocator.Get<ICelebrationResponseService>();
        }

        public void Redo()
        {
            _celebration.OrganizerId = _organizerId;
            _celebrationService.Update(_celebration);

            _celebrationResponseService.Create(_celebrationResponse);
            EventBus.FireEvent("ReloadAcceptCelebrationRequestTable");
            EventBus.FireEvent("ReloadCurrentOrganizatorCelebrationsTable");
        }

        public void Undo()
        {
            _celebration.OrganizerId = null;
            _celebration.Organizer = null;
            _celebrationService.Update(_celebration);

            _celebrationResponseService.Delete(_celebrationResponse.Id);
            EventBus.FireEvent("ReloadAcceptCelebrationRequestTable");
            EventBus.FireEvent("ReloadCurrentOrganizatorCelebrationsTable");
        }
    }

    public class CreateProposal : IUserCommand
    {
        private readonly CelebrationProposal _proposal;
        private readonly Notification _notification;

        private readonly ICelebrationProposalService _celebrationProposalService;
        private readonly INotificationService _notificationService;

        public CreateProposal(CelebrationProposal proposal, Notification notification)
        {
            _proposal = proposal;
            _notification = notification;

            _celebrationProposalService = ServiceLocator.Get<ICelebrationProposalService>();
            _notificationService = ServiceLocator.Get<INotificationService>();
        }

        public void Redo()
        {
            _celebrationProposalService.Create(_proposal);
            _notificationService.Create(_notification);
            EventBus.FireEvent("PreviewProposalsFromNotificationOrganizer", _proposal.CelebrationDetail);
        }

        public void Undo()
        {
            _celebrationProposalService.Delete(_proposal.Id);
            _notificationService.Delete(_notification.Id);
            EventBus.FireEvent("PreviewProposalsFromNotificationOrganizer", _proposal.CelebrationDetail);
        }
    }

    public class CreateComment : IUserCommand
    {
        private readonly ProposalComment _comment;
        private readonly Notification _notification;

        private readonly IProposalCommentService _proposalCommentService;
        private readonly INotificationService _notificationService;

        public CreateComment(ProposalComment comment, Notification notification)
        {
            _comment = comment;
            _notification = notification;

            _proposalCommentService = ServiceLocator.Get<IProposalCommentService>();
            _notificationService = ServiceLocator.Get<INotificationService>();
        }

        public void Redo()
        {
            _proposalCommentService.Create(_comment);
            _notificationService.Create(_notification);
            EventBus.FireEvent("PreviewCommentsFromNotificationOrganizer", _comment.CelebrationProposal);
        }

        public void Undo()
        {
            _proposalCommentService.Delete(_comment.Id);
            _notificationService.DeleteCommentNotification(_notification as NewCommentNotification);
            EventBus.FireEvent("PreviewCommentsFromNotificationOrganizer", _comment.CelebrationProposal);
        }
    }

    public class CancelCelebrationResponse : IUserCommand
    {
        private readonly CelebrationResponse _celebrationResponse;
        private readonly Celebration _celebration;
        private readonly Address _address;

        private readonly Notification _cancelNotification;
        private readonly IEnumerable<Notification> _notifications;

        private readonly ICelebrationResponseService _celebrationResponseService;
        private readonly ICrudService<Celebration> _celebrationService;
        private readonly INotificationService _notificationService;

        public CancelCelebrationResponse(CelebrationResponse celebrationResponse, Notification cancelNotification, IEnumerable<Notification> notifications)
        {
            _celebrationResponse = celebrationResponse;
            _celebration = _celebrationResponse.Celebration;
            _address = _celebration.Address;

            _cancelNotification = cancelNotification;
            _notifications = notifications;

            _celebrationResponseService = ServiceLocator.Get<ICelebrationResponseService>();
            _celebrationService = ServiceLocator.Get<ICrudService<Celebration>>();
            _notificationService = ServiceLocator.Get<INotificationService>();
        }

        public void Redo()
        {
            _celebrationResponseService.Delete(_celebrationResponse.Id);
            _celebrationService.Delete(_celebrationResponse.Id);
            _notificationService.Create(_cancelNotification);
            EventBus.FireEvent("ReloadCurrentOrganizatorCelebrationsTable");
        }

        public void Undo()
        {
            _celebration.AddressId = _address?.Id;
            _celebration.Address = null;
            _celebrationService.Create(_celebration);
            _celebrationResponseService.Create(_celebrationResponse);

            _notificationService.CreateRange(_notifications);
            _notificationService.Delete(_cancelNotification.Id);

            EventBus.FireEvent("ReloadCurrentOrganizatorCelebrationsTable");
        }
    }
}