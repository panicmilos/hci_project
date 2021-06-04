using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Organizator_Proslava.Model;
using Organizator_Proslava.ViewModel.CelebrationResponseForm;

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
        private readonly IDialogService _dialogService;
        private readonly ICollaboratorService _collaboratorService;

        public CelebrationProposalsTableViewModel(
            ProposalCommentsViewModel pcvm,
            ICollaboratorService collaboratorService,
            IDialogService dialogService)
        {
            _pcvm = pcvm;
            _collaboratorService = collaboratorService;
            _dialogService = dialogService;

            Comments = new RelayCommand<CelebrationProposal>(cd =>
            {
                _pcvm.CelebrationProposal = cd;
                _pcvm.ProposalComments = new ObservableCollection<ProposalComment>(cd.ProposalComments);
                EventBus.FireEvent("SwitchCelebrationProposalsViewModel", _pcvm);
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToCelebrationDetailsTable", CelebrationResponse));
        }
    }
}