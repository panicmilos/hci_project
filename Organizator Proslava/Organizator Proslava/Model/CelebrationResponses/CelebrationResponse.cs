using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Organizator_Proslava.Model.CelebrationResponses
{
    public class CelebrationResponse : BaseObservableEntity
    {
        private Guid _celebrationId;
        public Guid CelebrationId { get => _celebrationId; set => OnPropertyChanged(ref _celebrationId, value); }

        private Celebration _celebration;
        public virtual Celebration Celebration { get => _celebration; set => OnPropertyChanged(ref _celebration, value); }

        private Guid _organizerId;
        public Guid OrganizerId { get => _organizerId; set => OnPropertyChanged(ref _organizerId, value); }

        private Organizer _organizer;
        public virtual Organizer Organizer { get => _organizer; set => OnPropertyChanged(ref _organizer, value); }

        private List<CelebrationProposal> _celebrationProposals;
        public virtual List<CelebrationProposal> CelebrationProposals { get { return _celebrationProposals; } set { _celebrationProposals = value; OnPropertyChanged("CelebrationProposals"); } }

        [NotMapped]
        public DefaultDictionary<CelebrationDetail, List<CelebrationProposal>> CelebrationProposalsDict
        {
            get
            {
                var dictionaryOfProsals = new DefaultDictionary<CelebrationDetail, List<CelebrationProposal>>();
                foreach (var proposal in CelebrationProposals)
                {
                    dictionaryOfProsals[proposal.CelebrationDetail].Add(proposal);
                }

                return dictionaryOfProsals;
            }
        }

        public CelebrationResponse()
        {
            CelebrationProposals = new List<CelebrationProposal>();
        }
    }
}