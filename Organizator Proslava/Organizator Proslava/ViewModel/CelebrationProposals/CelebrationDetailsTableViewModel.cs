using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Utility;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CelebrationProposals
{
    public class CelebrationDetailsTableViewModel
    {
        public CelebrationResponse CelebrationResponse { get; set; }
        private CelebrationDetail _currentCelebrationDetail;

        public ICommand Preview { get; set; }
        public ICommand Proposals { get; set; }
        public ICommand Back { get; set; }

        private readonly IDialogService _dialogService;

        private readonly CelebrationProposalsTableViewModel _cptvm;

        public CelebrationDetailsTableViewModel(
            CelebrationProposalsTableViewModel cptvm,
            IDialogService dialogService)
        {
            _cptvm = cptvm;
            _dialogService = dialogService;

            Preview = new RelayCommand<CelebrationDetail>(cd => _dialogService.OpenDialog(new CelebrationDetailDialogViewModel(cd)));
            Proposals = new RelayCommand<CelebrationDetail>(cd =>
            {
                _currentCelebrationDetail = cd;
                _cptvm.CelebrationResponse = CelebrationResponse;
                _cptvm.CelebrationProposals = new ObservableCollection<CelebrationProposal>(CelebrationResponse.CelebrationProposalsDict[_currentCelebrationDetail]);
                EventBus.FireEvent("SwitchCelebrationProposalsViewModel", _cptvm);
            });

            EventBus.RegisterHandler("BackToProposalsTableForClient", () => EventBus.FireEvent("SwitchCelebrationProposalsViewModel", _cptvm));

            Back = new RelayCommand(() => EventBus.FireEvent("BackToClientPage"));
        }
    }
}