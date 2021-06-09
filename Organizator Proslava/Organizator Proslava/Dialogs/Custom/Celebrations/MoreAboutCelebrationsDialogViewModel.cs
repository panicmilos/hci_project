using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CelebrationResponseForm;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Celebrations
{
    public class MoreAboutCelebrationsDialogViewModel : DialogViewModelBase<DialogResults>
    {
        public Celebration Celebration { get; set; }
        public ICommand Back { get; set; }
        public ICommand Details { get; set; }

        public CelebrationsDetailsTableDialogViewModel CelebrationsDetailsTableDialogViewModel { get; set; }
        public CelebrationDetailDialogViewModel CelebrationDetailDialogViewModel { get; set; }
        public CelebrationsProposalsTableDialogViewModel CelebrationsProposalsTableDialogViewModel { get; set; }
        public ProposalCommentsViewModel ProposalCommentsViewModel { get; set; }

        public MoreAboutCelebrationsDialogViewModel(
            CelebrationDetailDialogViewModel celebrationDetailDialogViewModel,
            CelebrationsProposalsTableDialogViewModel celebrationsProposalsTableDialogViewModel,
            CelebrationsDetailsTableDialogViewModel celebrationsDetailsTableDialogViewModel,
            ProposalCommentsViewModel proposalCommentsViewModel) :
            base("Pregled proslave", 550, 450)
        {
            CelebrationsDetailsTableDialogViewModel = celebrationsDetailsTableDialogViewModel;
            CelebrationDetailDialogViewModel = celebrationDetailDialogViewModel;
            CelebrationsProposalsTableDialogViewModel = celebrationsProposalsTableDialogViewModel;
            ProposalCommentsViewModel = proposalCommentsViewModel;

            Back = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, DialogResults.Undefined));

            Details = new RelayCommand(() =>
            {
                CelebrationsDetailsTableDialogViewModel.Celebration = Celebration;
                EventBus.FireEvent("CelebrationDetails");
                /*DetailsDialogViewModel ddvm = new DetailsDialogViewModel(CelebrationsDetailsTableDialogViewModel,
                    _celebrationResponseService, CelebrationDetailDialogViewModel,
                    CelebrationsProposalsTableDialogViewModel,
                    ProposalCommentsViewModel);
                ddvm.CurrentCelebration = Celebration;
                _dialogService.OpenDialog(ddvm);*/
            });
        }
    }
}