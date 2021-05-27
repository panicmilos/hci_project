using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CelebrationResponseForm
{
    public class ProposalCommentsViewModel : ObservableEntity
    {
        public CelebrationProposal CelebrationProposal { get; set; }

        public ICommand Back { get; set; }

        public ProposalCommentsViewModel()
        {
            Back = new RelayCommand(() => EventBus.FireEvent("BackToProposalsTableForOrganizer"));
        }
    }
}