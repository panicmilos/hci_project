using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CelebrationResponseForm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Celebrations
{
    public class CelebrationsProposalsTableDialogViewModel
    {
        public ObservableCollection<CelebrationProposal> CelebrationProposals { get; set; }
        public CelebrationResponse CelebrationResponse { get; set; }

        public ICommand Comments { get; set; }
        public ICommand Back { get; set; }

        public CelebrationsProposalsTableDialogViewModel(ProposalCommentsViewModel pcvm,
            ICelebrationProposalService celebrationProposalService,
            IDialogService dialogService)
        {
            Comments = new RelayCommand<CelebrationProposal>(cp =>
            {
                EventBus.FireEvent("ProposalsComments", cp);
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToDetailsTable"));
        }
    }
}