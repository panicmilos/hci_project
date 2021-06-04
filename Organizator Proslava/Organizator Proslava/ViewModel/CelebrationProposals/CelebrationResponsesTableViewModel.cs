using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Windows.Input;
using Organizator_Proslava.Model;
using Organizator_Proslava.ViewModel.CelebrationResponseForm;

namespace Organizator_Proslava.ViewModel.CelebrationProposals
{
    public class CelebrationResponsesTableViewModel
    {
        public ObservableCollection<CelebrationResponse> CelebrationResponses { get; set; }
        private CelebrationResponse _currentCelebrationResponse;

        private Celebration _celebration;
        public Celebration Celebration
        {
            get => _celebration;
            set
            {
                _celebration = value;
                CelebrationResponses =
                    new ObservableCollection<CelebrationResponse>(
                        _celebrationResponseService.ReadForCelebration(_celebration.Id));
            }
        }

        public ICommand Details { get; set; }
        public ICommand Back { get; set; }

        private readonly ICelebrationResponseService _celebrationResponseService;
        private readonly IDialogService _dialogService;
        
        private readonly CelebrationDetailsTableViewModel _cdtvm;

        public CelebrationResponsesTableViewModel(
            CelebrationDetailsTableViewModel cdtvm,
            ICelebrationResponseService celebrationResponseService,
            IDialogService dialogService)
        {
            _cdtvm = cdtvm;

            _celebrationResponseService = celebrationResponseService;
            _dialogService = dialogService;

            Details = new RelayCommand<CelebrationResponse>(cr =>
            {
                _currentCelebrationResponse = cr;
                _cdtvm.CelebrationResponse = _currentCelebrationResponse;
                EventBus.FireEvent("SwitchCelebrationProposalsViewModel", _cdtvm);
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToClientPage"));
        }
    }
}