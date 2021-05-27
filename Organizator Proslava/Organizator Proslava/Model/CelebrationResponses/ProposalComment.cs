using System;

namespace Organizator_Proslava.Model.CelebrationResponses
{
    public class ProposalComment : BaseObservableEntity
    {
        public Guid _writerId;
        public Guid WriterId { get => _writerId; set => OnPropertyChanged(ref _writerId, value); }

        private BaseUser _writer;
        public virtual BaseUser Writer { get => _writer; set => OnPropertyChanged(ref _writer, value); }

        private Guid _celebrationProposalId;
        public Guid CelebrationProposalId { get => _celebrationProposalId; set => OnPropertyChanged(ref _celebrationProposalId, value); }

        private CelebrationProposal _celebrationProposal;
        public virtual CelebrationProposal CelebrationProposal { get => _celebrationProposal; set => OnPropertyChanged(ref _celebrationProposal, value); }

        private string _content;
        public string Content { get => _content; set => OnPropertyChanged(ref _content, value); }
    }
}