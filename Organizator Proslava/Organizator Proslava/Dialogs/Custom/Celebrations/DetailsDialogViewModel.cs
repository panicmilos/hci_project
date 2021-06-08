using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CelebrationProposals;
using Organizator_Proslava.ViewModel.CelebrationResponseForm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizator_Proslava.Dialogs.Custom.Celebrations
{
    public class DetailsDialogViewModel : NavigableDialogViewModelBase<DialogResults>
    {
        public CelebrationsDetailsTableDialogViewModel CelebrationsDetailsTableDialogViewModel { get; set; }
        public CelebrationDetailDialogViewModel CelebrationDetailDialogViewModel { get; set; }
        public CelebrationsProposalsTableDialogViewModel CelebrationsProposalsTableDialogViewModel { get; set; }
        public MoreAboutCelebrationsDialogViewModel MoreAboutCelebrationsDialogViewModel { get; set; }

        public ProposalCommentsViewModel ProposalCommentsViewModel { get; set; }

        public Celebration CurrentCelebration { get; set; }

        private readonly ICelebrationResponseService _celebrationResponseService;

        public DetailsDialogViewModel(CelebrationsDetailsTableDialogViewModel celebrationsDetailsTableDialogViewModel,

            ICelebrationResponseService celebrationResponseService,
            CelebrationDetailDialogViewModel celebrationDetailDialogViewModel,
            CelebrationsProposalsTableDialogViewModel celebrationsProposalsTableDialogViewModel,
            ProposalCommentsViewModel proposalCommentsViewModel,
            MoreAboutCelebrationsDialogViewModel moreAboutCelebrationsDialogView) :
            base("Pregled detalja proslave", 650, 490)
        {

            _celebrationResponseService = celebrationResponseService;

            CelebrationsDetailsTableDialogViewModel = celebrationsDetailsTableDialogViewModel;
            CelebrationDetailDialogViewModel = celebrationDetailDialogViewModel;
            CelebrationsProposalsTableDialogViewModel = celebrationsProposalsTableDialogViewModel;
            ProposalCommentsViewModel = proposalCommentsViewModel;
            MoreAboutCelebrationsDialogViewModel = moreAboutCelebrationsDialogView;

            Switch(MoreAboutCelebrationsDialogViewModel);

            RegisterHandlerToEventBus();
        }

        public void RegisterHandlerToEventBus()
        {
            ProposalCommentsViewModel.ChangeBack();

            EventBus.RegisterHandler("CelebrationDetails", () => Switch(CelebrationsDetailsTableDialogViewModel));

            EventBus.RegisterHandler("BackToMoreInfoAboutCelebration", () => Switch(MoreAboutCelebrationsDialogViewModel));

            EventBus.RegisterHandler("CelebrationDetailPreview", detail =>
            {
                CelebrationDetailDialogViewModel.IsBack = true;
                CelebrationDetailDialogViewModel.IsClose = false;
                CelebrationDetailDialogViewModel.CelebrationDetail = detail as CelebrationDetail;
                Switch(CelebrationDetailDialogViewModel);
            });

            EventBus.RegisterHandler("BackToDetailsTable", () => {
                CelebrationsDetailsTableDialogViewModel.Celebration = CurrentCelebration;
                CelebrationsDetailsTableDialogViewModel.CelebrationDetails = new ObservableCollection<CelebrationDetail>(CurrentCelebration.CelebrationDetails);
                Switch(CelebrationsDetailsTableDialogViewModel);
            }
            );

            EventBus.RegisterHandler("CelebrationProposals", proposals =>
            {
                CelebrationsProposalsTableDialogViewModel.CelebrationProposals = proposals as ObservableCollection<CelebrationProposal>;
                CelebrationsProposalsTableDialogViewModel.CelebrationResponse = _celebrationResponseService.ReadForCelebration(CurrentCelebration.Id);
                Switch(CelebrationsProposalsTableDialogViewModel);
            });

            EventBus.RegisterHandler("ProposalsComments", celebrationProposal =>
            {
                ProposalCommentsViewModel.CelebrationProposal = celebrationProposal as CelebrationProposal;
                ProposalCommentsViewModel.ProposalComments = new ObservableCollection<ProposalComment>((celebrationProposal as CelebrationProposal).ProposalComments);
                Switch(ProposalCommentsViewModel);
            });

            EventBus.RegisterHandler("BackToProposalsTableForAdmin", () =>
            {
                Switch(CelebrationsProposalsTableDialogViewModel);
            });
        }
    }
}
