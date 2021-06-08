using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Alert;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CelebrationResponseForm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

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
        public ICommand Accept { get; set; }
        public ICommand Reject { get; set; }

        private readonly ProposalCommentsViewModel _pcvm;

        private readonly ICelebrationProposalService _celebrationProposalService;

        public CelebrationProposalsTableViewModel(
            ProposalCommentsViewModel pcvm,
            ICelebrationProposalService celebrationProposalService,
            IDialogService dialogService)
        {
            _pcvm = pcvm;

            _celebrationProposalService = celebrationProposalService;

            Comments = new RelayCommand<CelebrationProposal>(cp =>
            {
                _pcvm.CelebrationProposal = cp;
                _pcvm.ProposalComments = new ObservableCollection<ProposalComment>(cp.ProposalComments);
                EventBus.FireEvent("SwitchCelebrationProposalsViewModel", _pcvm);
            });

            Accept = new RelayCommand<CelebrationProposal>(cp =>
            {
                if (cp.Status != CelebrationProposalStatus.Neobradjen)
                {
                    dialogService.OpenDialog(new AlertDialogViewModel("Predlog već obrađen",
                        "Predlog koji pokušavate da prihvatite je već obrađen."));
                    return;
                }

                if (dialogService.OpenDialog(new OptionDialogViewModel("Prihvatanje predloga",
                    "Da li ste sigurni da želite da prihvatite predlog?")) != DialogResults.Yes)
                    return;

                cp.Status = CelebrationProposalStatus.Prihvacen;
                celebrationProposalService.Update(cp);
                RefreshTable();
            });

            Reject = new RelayCommand<CelebrationProposal>(cp =>
            {
                if (cp.Status != CelebrationProposalStatus.Neobradjen)
                {
                    dialogService.OpenDialog(new AlertDialogViewModel("Predlog već obrađen",
                        "Predlog koji pokušavate da odbijete je već obrađen."));
                    return;
                }

                if (dialogService.OpenDialog(new OptionDialogViewModel("Odbijanje predloga",
                    "Da li ste sigurni da želite da odbijete predlog?")) != DialogResults.Yes)
                    return;

                cp.Status = CelebrationProposalStatus.Odbijen;
                celebrationProposalService.Update(cp);
                RefreshTable();
            });

            EventBus.RegisterHandler("PreviewCommentsFromNotificationClient", cp => Comments.Execute(cp));

            Back = new RelayCommand(() => EventBus.FireEvent("BackToCelebrationDetailsTable", CelebrationResponse));
        }

        public void RefreshTable()
        {
            if (CelebrationProposals == null)
            {
                CelebrationProposals = new ObservableCollection<CelebrationProposal>();
            }
            CelebrationProposals.Clear();

            _celebrationProposalService.Read().ToList().ForEach(cp => CelebrationProposals.Add(cp));
        }
    }
}