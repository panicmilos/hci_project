using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CelebrationProposals;
using Organizator_Proslava.ViewModel.CelebrationResponseForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Celebrations
{
    public class MoreAboutCelebrationsDialogViewModel : DialogViewModelBase<DialogResults>
    {
        public Celebration Celebration { get; set; }
        public ICommand Back { get; set; }
        public ICommand Details { get; set; }

        private readonly IDialogService _dialogService;

        private readonly ICelebrationResponseService _celebrationResponseService;

        public CelebrationsDetailsTableDialogViewModel CelebrationsDetailsTableDialogViewModel { get; set; }
        public CelebrationDetailDialogViewModel CelebrationDetailDialogViewModel { get; set; }
        public CelebrationsProposalsTableDialogViewModel CelebrationsProposalsTableDialogViewModel { get; set; }
        public ProposalCommentsViewModel ProposalCommentsViewModel { get; set; }

        public MoreAboutCelebrationsDialogViewModel(Celebration celebration,
            ICelebrationResponseService celebrationResponseService,
            IDialogService dialogService,
            CelebrationDetailDialogViewModel celebrationDetailDialogViewModel,
            CelebrationsProposalsTableDialogViewModel celebrationsProposalsTableDialogViewModel,
            CelebrationsDetailsTableDialogViewModel celebrationsDetailsTableDialogViewModel,
            ProposalCommentsViewModel proposalCommentsViewModel) :
            base("Pregled proslave", 550, 450)
        {

            _celebrationResponseService = celebrationResponseService;
            _dialogService = dialogService;

            CelebrationsDetailsTableDialogViewModel = celebrationsDetailsTableDialogViewModel;
            CelebrationDetailDialogViewModel = celebrationDetailDialogViewModel;
            CelebrationsProposalsTableDialogViewModel = celebrationsProposalsTableDialogViewModel;
            ProposalCommentsViewModel = proposalCommentsViewModel;

            Celebration = celebration;
            Back = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, DialogResults.Undefined));

            Details = new RelayCommand(() =>
            {
                CelebrationsDetailsTableDialogViewModel.Celebration = Celebration;
                DetailsDialogViewModel ddvm = new DetailsDialogViewModel(CelebrationsDetailsTableDialogViewModel,
                    _celebrationResponseService, CelebrationDetailDialogViewModel,
                    CelebrationsProposalsTableDialogViewModel,
                    ProposalCommentsViewModel);
                ddvm.CurrentCelebration = Celebration;
                _dialogService.OpenDialog(ddvm);
            });
        }
    }
}
