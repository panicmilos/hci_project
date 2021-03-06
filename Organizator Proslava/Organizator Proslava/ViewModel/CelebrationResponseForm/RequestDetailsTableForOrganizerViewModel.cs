using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.UserCommands;
using Organizator_Proslava.Utility;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CelebrationResponseForm
{
    public class RequestDetailsTableForOrganizerViewModel
    {
        public CelebrationResponse CelebrationResponse { get; set; }
        private CelebrationDetail _currentCelebrationDetail;

        public ICommand Preview { get; set; }
        public ICommand Proposals { get; set; }
        public ICommand Back { get; set; }

        private readonly ICelebrationProposalService _celebrationProposalService;
        private readonly INotificationService _notificationService;
        private readonly IDialogService _dialogService;

        private readonly ProposalsTableForOrganizerViewModel _ptfovm;

        public RequestDetailsTableForOrganizerViewModel(
            ProposalsTableForOrganizerViewModel ptfovm,
            ICelebrationProposalService celebrationProposalService,
            INotificationService notificationService,
            IDialogService dialogService)
        {
            _ptfovm = ptfovm;
            _celebrationProposalService = celebrationProposalService;
            _notificationService = notificationService;
            _dialogService = dialogService;

            Preview = new RelayCommand<CelebrationDetail>(cd =>
            {
                CelebrationDetailDialogViewModel cddvm = new CelebrationDetailDialogViewModel();
                cddvm.CelebrationDetail = cd;
                cddvm.IsBack = false;
                cddvm.IsClose = true;
                _dialogService.OpenDialog(cddvm);
            }
            );
            EventBus.RegisterHandler("PreviewDetailFromNotificationOrganizer", cd => Preview.Execute(cd));

            Proposals = new RelayCommand<CelebrationDetail>(cd =>
            {
                _currentCelebrationDetail = cd;
                if (_ptfovm.CelebrationProposals == null)
                {
                    _ptfovm.CelebrationProposals = new ObservableCollection<CelebrationProposal>();
                }

                _ptfovm.CelebrationResponse = CelebrationResponse;
                _ptfovm.CelebrationProposals.Clear();
                CelebrationResponse.CelebrationProposalsDict[cd].ToList().ForEach(cp => _ptfovm.CelebrationProposals.Add(cp));
                EventBus.FireEvent("SwitchCelebrationResponseFormViewModel", _ptfovm);
            });

            EventBus.RegisterHandler("PreviewProposalsFromNotificationOrganizer", cd => Proposals.Execute(cd));

            Back = new RelayCommand(() => EventBus.FireEvent("BackToRequestLongPreviewForOranizer"));

            EventBus.RegisterHandler("BackToProposalsTableForOrganizer", () => EventBus.FireEvent("SwitchCelebrationResponseFormViewModel", _ptfovm));

            EventBus.RegisterHandler("CreateNewProposal", proposalObject =>
            {
                var proposal = proposalObject as CelebrationProposal;
                proposal.CelebrationDetail = _currentCelebrationDetail;
                proposal.CelebrationResponse = CelebrationResponse;
                var createdProposal = _celebrationProposalService.Create(proposal);

                var createdNotification = _notificationService.Create(new NewProposalNotification
                {
                    ForUserId = CelebrationResponse.Celebration.Client.Id,
                    ProposalId = proposal.Id,
                    CelebrationResponseId = CelebrationResponse.Id,
                });

                GlobalStore.ReadObject<IUserCommandManager>("userCommands").Add(new CreateProposal(createdProposal, createdNotification));
            });
        }
    }
}