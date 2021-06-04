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

        private CelebrationResponse _celebrationResponse;
        public virtual CelebrationResponse CelebrationResponse { get => _celebrationResponse; set => OnPropertyChanged(ref _celebrationResponse, value); }
    }

    public class NewCommentNotification : Notification
    {
        private Guid _proposalId;
        public Guid ProposalId { get => _proposalId; set => OnPropertyChanged(ref _proposalId, value); }

        private CelebrationProposal _proposal;
        public virtual CelebrationProposal Proposal { get => _proposal; set => OnPropertyChanged(ref _proposal, value); }

        private int _numOfComments;
        public int NumOfComments { get => _numOfComments; set => OnPropertyChanged(ref _numOfComments, value); }

        public override string ToString()
        {
            // Dodati stranu korisnika
            var sufix = NumOfComments > 1 ? (NumOfComments > 3 ? "ih" : "a") : "";
            return $"Imate {NumOfComments} nov{sufix} komentar na \"{Proposal.Title}\" predlogu proslave koju organizujete za {CelebrationResponse.Celebration.Client.FullName}.";
        }
    }

    public class NewProposalNotification : Notification
    {
        private Guid _proposalId;
        public Guid ProposalId { get => _proposalId; set => OnPropertyChanged(ref _proposalId, value); }

        private CelebrationProposal _proposal;
        public virtual CelebrationProposal Proposal { get => _proposal; set => OnPropertyChanged(ref _proposal, value); }

        public override string ToString()
        {
            return $"Imate nov predlog(\"{Proposal.Title}\") na proslavi koju Vam organizuje {CelebrationResponse.Celebration.Organizer.FullName}.";
        }
    }

    public class NewDetailNotification : Notification
    {
        private Guid _detailId;
        public Guid DetailId { get => _detailId; set => OnPropertyChanged(ref _detailId, value); }

        private CelebrationDetail _detail;
        public virtual CelebrationDetail Detail { get => _detail; set => OnPropertyChanged(ref _detail, value); }

        public override string ToString()
        {
            return $"Imate nov detalj(\"{Detail.Title}\") na proslavi koju organizujete za {CelebrationResponse.Celebration.Client.FullName}.";
        }
    }
}