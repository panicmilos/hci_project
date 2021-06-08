using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CelebrationResponseForm
{
    public class ProposalsTableForOrganizerViewModel
    {
        public ObservableCollection<CelebrationProposal> CelebrationProposals { get; set; }

        public ICommand Preview { get; set; }
        public ICommand Comments { get; set; }
        public ICommand Add { get; set; }
        public ICommand Back { get; set; }

        private readonly ProposalCommentsViewModel _pcvm;
        private readonly IDialogService _dialogService;
        private readonly ICollaboratorService _collaboratorService;

        public ProposalsTableForOrganizerViewModel(
            ProposalCommentsViewModel pcvm,
            ICollaboratorService collaboratorService,
            IDialogService dialogService)
        {
            _pcvm = pcvm;
            _collaboratorService = collaboratorService;
            _dialogService = dialogService;

            Comments = new RelayCommand<CelebrationProposal>(cp =>
            {
                _pcvm.CelebrationProposal = cp;
                if (_pcvm.ProposalComments == null)
                {
                    _pcvm.ProposalComments = new ObservableCollection<ProposalComment>();
                }
                _pcvm.ProposalComments.Clear();
                cp.ProposalComments.ToList().ForEach(pc => _pcvm.ProposalComments.Add(pc));
                EventBus.FireEvent("SwitchCelebrationResponseFormViewModel", _pcvm);
            });

            EventBus.RegisterHandler("PreviewCommentsFromNotificationOrganizer", cp => Comments.Execute(cp));

            Add = new RelayCommand(() =>
            {
                var proposal = _dialogService.OpenDialog(new CelebrationProposalDialogViewModel(_collaboratorService, _dialogService));
                if (proposal != null)
                {
                    EventBus.FireEvent("CreateNewProposal", proposal);
                    CelebrationProposals.Add(proposal);
                }
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToRequestDetailsForOrganizer"));
        }
    }
}