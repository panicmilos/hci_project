using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Alert;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CelebrationResponseForm;
using System;
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
        private readonly IDialogService _dialogService;
        private readonly INotificationService _notificationService;

        public CelebrationProposalsTableViewModel(
            ProposalCommentsViewModel pcvm,
            ICelebrationProposalService celebrationProposalService,
            IDialogService dialogService,
            INotificationService notificationService)
        {
            _pcvm = pcvm;

            _celebrationProposalService = celebrationProposalService;
            _dialogService = dialogService;
            _notificationService = notificationService;

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
                    _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje",
                        "Ponuda koju pokušavate da prihvatite je već obrađena."));
                    return;
                }

                if (_dialogService.OpenDialog(new OptionDialogViewModel("Potvrda",
                    "Da li ste sigurni da želite da prihvatite ponudu?")) != DialogResults.Yes)
                    return;

                cp.Status = CelebrationProposalStatus.Prihvacen;
                celebrationProposalService.Update(cp);
                RefreshTableForDetail(cp.CelebrationDetailId);

                _notificationService.Create(new ChangedStatusOfProposalNotification
                {
                    ForUserId = CelebrationResponse.Celebration.Organizer.Id,
                    CelebrationResponseId = CelebrationResponse.Id,
                    ProposalId = cp.Id
                });
            });

            Reject = new RelayCommand<CelebrationProposal>(cp =>
            {
                if (cp.Status != CelebrationProposalStatus.Neobradjen)
                {
                    _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje",
                        "Ponuda koju pokušavate da odbijete je već obrađena."));
                    return;
                }

                if (_dialogService.OpenDialog(new OptionDialogViewModel("Potvrda",
                    "Da li ste sigurni da želite da odbijete ponudu?")) != DialogResults.Yes)
                    return;

                cp.Status = CelebrationProposalStatus.Odbijen;
                celebrationProposalService.Update(cp);
                RefreshTableForDetail(cp.CelebrationDetailId);

                _notificationService.Create(new ChangedStatusOfProposalNotification
                {
                    ForUserId = CelebrationResponse.Celebration.Organizer.Id,
                    CelebrationResponseId = CelebrationResponse.Id,
                    ProposalId = cp.Id
                });
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

        public void RefreshTableForDetail(Guid detailId)
        {
            if (CelebrationProposals == null)
            {
                CelebrationProposals = new ObservableCollection<CelebrationProposal>();
            }
            CelebrationProposals.Clear();

            _celebrationProposalService.ReadFor(detailId).ToList().ForEach(cp => CelebrationProposals.Add(cp));
        }
    }
}