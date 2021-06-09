using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CelebrationResponseForm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.OrganizatorHome
{
    public class OrganizersPastCelebrationsTableViewModel
    {
        public ObservableCollection<Celebration> PastCelebrations { get; set; }

        public ICommand Preview { get; set; }
        public ICommand Back { get; set; }

        private readonly ICelebrationService _celebrationService;
        private readonly ICelebrationResponseService _celebrationResponseService;
        private readonly IDialogService _dialogService;

        public CelebrationsDetailsTableDialogViewModel CelebrationsDetailsTableDialogViewModel { get; set; }
        public CelebrationDetailDialogViewModel CelebrationDetailDialogViewModel { get; set; }
        public CelebrationsProposalsTableDialogViewModel CelebrationsProposalsTableDialogViewModel { get; set; }
        public ProposalCommentsViewModel ProposalCommentsViewModel { get; set; }
        public MoreAboutCelebrationsDialogViewModel MoreAboutCelebrationsDialogViewModel { get; set; }

        public OrganizersPastCelebrationsTableViewModel(ICelebrationService celebrationService, 
            ICelebrationResponseService celebrationResponseService,
            IDialogService dialogService,
            CelebrationDetailDialogViewModel celebrationDetailDialogViewModel,
            CelebrationsProposalsTableDialogViewModel celebrationsProposalsTableDialogViewModel,
            CelebrationsDetailsTableDialogViewModel celebrationsDetailsTableDialogViewModel,
            ProposalCommentsViewModel proposalCommentsViewModel,
            MoreAboutCelebrationsDialogViewModel moreAboutCelebrationsDialogViewModel)
        {
            _celebrationService = celebrationService;
            _celebrationResponseService = celebrationResponseService;
            _dialogService = dialogService;

            CelebrationsDetailsTableDialogViewModel = celebrationsDetailsTableDialogViewModel;
            CelebrationDetailDialogViewModel = celebrationDetailDialogViewModel;
            CelebrationsProposalsTableDialogViewModel = celebrationsProposalsTableDialogViewModel;
            ProposalCommentsViewModel = proposalCommentsViewModel;
            MoreAboutCelebrationsDialogViewModel = moreAboutCelebrationsDialogViewModel;

            PastCelebrations = new ObservableCollection<Celebration>();

            Reload();

            Preview = new RelayCommand<Celebration>(celebration => {
                CelebrationsDetailsTableDialogViewModel.Celebration = celebration;
                CelebrationsDetailsTableDialogViewModel.CelebrationDetails = new ObservableCollection<CelebrationDetail>(celebration.CelebrationDetails);
                MoreAboutCelebrationsDialogViewModel.Celebration = celebration;
                DetailsDialogViewModel ddvm = new DetailsDialogViewModel(CelebrationsDetailsTableDialogViewModel, _celebrationResponseService,
                    celebrationDetailDialogViewModel, CelebrationsProposalsTableDialogViewModel, ProposalCommentsViewModel,
                    MoreAboutCelebrationsDialogViewModel)
                { CurrentCelebration = celebration };
                _dialogService.OpenDialog(ddvm);
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToCurrentCelebrationsForOrganizer"));
        }

        public void Reload()
        {
            PastCelebrations.Clear();
            _celebrationService.ReadPastForOrganizer(GlobalStore.ReadObject<BaseUser>("loggedUser").Id).ToList().ForEach(cr => PastCelebrations.Add(cr));
        }
    }
}
