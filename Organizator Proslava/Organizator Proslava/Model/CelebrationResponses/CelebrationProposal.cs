using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Services.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Organizator_Proslava.Model.CelebrationResponses
{
    public class CelebrationProposal : BaseObservableEntity
    {
        private Guid _celebrationResponseId;
        public Guid CelebrationResponseId { get => _celebrationResponseId; set => OnPropertyChanged(ref _celebrationResponseId, value); }

        private CelebrationResponse _celebrationResponse;
        public virtual CelebrationResponse CelebrationResponse { get => _celebrationResponse; set => OnPropertyChanged(ref _celebrationResponse, value); }

        private Guid _celebrationDetailId;
        public Guid CelebrationDetailId { get => _celebrationDetailId; set => OnPropertyChanged(ref _celebrationDetailId, value); }

        private CelebrationDetail _celebrationDetail;
        public virtual CelebrationDetail CelebrationDetail { get => _celebrationDetail; set => OnPropertyChanged(ref _celebrationDetail, value); }

        private string _title;
        public string Title { get => _title; set => OnPropertyChanged(ref _title, value); }

        private string _content;
        public string Content { get => _content; set => OnPropertyChanged(ref _content, value); }

        private CelebrationProposalStatus _status;
        public CelebrationProposalStatus Status { get => _status; set => OnPropertyChanged(ref _status, value); }

        private Guid _collaboratorId;
        public Guid CollaboratorId { get => _collaboratorId; set => OnPropertyChanged(ref _collaboratorId, value); }

        private Collaborator _collaborator;
        public virtual Collaborator Collaborator { get => _collaborator; set => OnPropertyChanged(ref _collaborator, value); }

        private Guid? _celebrationHallId;
        public Guid? CelebrationHallId { get => _celebrationHallId; set => OnPropertyChanged(ref _celebrationHallId, value); }

        private CelebrationHall _celebrationHall;
        public virtual CelebrationHall CelebrationHall { get => _celebrationHall; set => OnPropertyChanged(ref _celebrationHall, value); }

        private ProposedService _proposedService;
        public virtual ProposedService ProposedService { get => _proposedService; set => OnPropertyChanged(ref _proposedService, value); }

        private List<ProposalComment> _proposalComments;

        public virtual List<ProposalComment> ProposalComments
        {
            get { return _proposalComments; }
            set { _proposalComments = value; OnPropertyChanged("ProposalComments"); }
        }

        public CelebrationProposal()
        {
            ProposalComments = new List<ProposalComment>();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(_title);
            builder.AppendLine();
            builder.AppendLine(ProposedService.ToString());
            builder.AppendLine();
            builder.AppendLine("====================");
            builder.AppendLine(_content);

            return builder.ToString();
        }
    }
}