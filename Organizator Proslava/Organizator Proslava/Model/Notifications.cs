using Organizator_Proslava.Model.CelebrationResponses;
using System;

namespace Organizator_Proslava.Model
{
    public class Notification : BaseObservableEntity
    {
        private Guid _forUserId;
        public Guid ForUserId { get => _forUserId; set => OnPropertyChanged(ref _forUserId, value); }

        private Guid _celebrationResponseId;
        public Guid CelebrationResponseId { get => _celebrationResponseId; set => OnPropertyChanged(ref _celebrationResponseId, value); }
    }

    public class NewCommentNotification : Notification
    {
        private Guid _proposalId;
        public Guid ProposalId { get => _proposalId; set => OnPropertyChanged(ref _proposalId, value); }

        private CelebrationProposal _proposal;
        public virtual CelebrationProposal Proposal { get => _proposal; set => OnPropertyChanged(ref _proposal, value); }

        private int _numOfComments;
        public int NumOfComments { get => _numOfComments; set => OnPropertyChanged(ref _numOfComments, value); }
    }

    public class NewProposalNotification : Notification
    {
        private Guid _proposalId;
        public Guid ProposalId { get => _proposalId; set => OnPropertyChanged(ref _proposalId, value); }

        private CelebrationProposal _proposal;
        public virtual CelebrationProposal Proposal { get => _proposal; set => OnPropertyChanged(ref _proposal, value); }
    }

    public class NewDetailNotification : Notification
    {
        private Guid _detailId;
        public Guid DetailId { get => _detailId; set => OnPropertyChanged(ref _detailId, value); }

        private CelebrationDetail _detail;
        public virtual CelebrationDetail Detail { get => _detail; set => OnPropertyChanged(ref _detail, value); }
    }
}