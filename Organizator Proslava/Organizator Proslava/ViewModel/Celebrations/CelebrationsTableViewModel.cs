using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
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
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.Celebrations
{
    public class CelebrationsTableViewModel
    {
        public ObservableCollection<Celebration> Celebrations { get; set; }

        public ICommand Preview { get; set; }

        public ICommand Back { get; set; }

        private readonly ICelebrationService _celebrationService;
        private readonly IDialogService _dialogService;

        private readonly ICelebrationResponseService _celebrationResponseService;
        public CelebrationsDetailsTableDialogViewModel CelebrationsDetailsTableDialogViewModel { get; set; }
        public CelebrationDetailDialogViewModel CelebrationDetailDialogViewModel { get; set; }
        public CelebrationsProposalsTableDialogViewModel CelebrationsProposalsTableDialogViewModel { get; set; }
        public ProposalCommentsViewModel ProposalCommentsViewModel { get; set; }

        public CelebrationsTableViewModel(ICelebrationService celebrationService,
            IDialogService dialogService,
            ICelebrationResponseService celebrationResponseService,
            CelebrationDetailDialogViewModel celebrationDetailDialogViewModel,
            CelebrationsProposalsTableDialogViewModel celebrationsProposalsTableDialogViewModel,
            CelebrationsDetailsTableDialogViewModel celebrationsDetailsTableDialogViewModel,
            ProposalCommentsViewModel proposalCommentsViewModel)
        {
            _celebrationService = celebrationService;
            _dialogService = dialogService;

            _celebrationResponseService = celebrationResponseService;

            CelebrationsDetailsTableDialogViewModel = celebrationsDetailsTableDialogViewModel;
            CelebrationDetailDialogViewModel = celebrationDetailDialogViewModel;
            CelebrationsProposalsTableDialogViewModel = celebrationsProposalsTableDialogViewModel;
            ProposalCommentsViewModel = proposalCommentsViewModel;

            Celebrations = new ObservableCollection<Celebration>(_celebrationService.Read());

            Back = new RelayCommand(() => EventBus.FireEvent("AdminLogin"));

            Preview = new RelayCommand<Celebration>(celebration => {
                CelebrationsDetailsTableDialogViewModel.Celebration = celebration;
                CelebrationsDetailsTableDialogViewModel.CelebrationDetails = new ObservableCollection<CelebrationDetail>(celebration.CelebrationDetails);
                _dialogService.OpenDialog(new MoreAboutCelebrationsDialogViewModel(celebration, _celebrationResponseService,
                     _dialogService,
                    celebrationDetailDialogViewModel, CelebrationsProposalsTableDialogViewModel, CelebrationsDetailsTableDialogViewModel, ProposalCommentsViewModel));
            });;
        }
    }
}
