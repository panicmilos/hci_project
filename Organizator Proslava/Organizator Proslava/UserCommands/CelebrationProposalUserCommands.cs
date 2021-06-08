﻿using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Ninject;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System;

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
}