using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CelebrationResponseForm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CelebrationProposals
{
    public class CelebrationProposalsTableViewModel
    {
        public ObservableCollection<CelebrationProposal> CelebrationProposals { get; set; }
        public CelebrationResponse CelebrationResponse { get; set; }

        public ICommand Preview { get; set; }
        public ICommand Comments { get; set; }
        public ICommand Add { get; set; }
        public ICommand Back { get; set; }

        private readonly ProposalCommentsViewModel _pcvm;

        public CelebrationProposalsTableViewModel(
            ProposalCommentsViewModel pcvm)
        {
            _pcvm = pcvm;

            Comments = new RelayCommand<CelebrationProposal>(cp =>
            {
                _pcvm.CelebrationProposal = cp;
                _pcvm.ProposalComments = new ObservableCollection<ProposalComment>(cp.ProposalComments);
                EventBus.FireEvent("SwitchCelebrationProposalsViewModel", _pcvm);
            });

            EventBus.RegisterHandler("PreviewCommentsFromNotificationClient", cp => Comments.Execute(cp));

            Back = new RelayCommand(() => EventBus.FireEvent("BackToCelebrationDetailsTable", CelebrationResponse));
        }
    }
}