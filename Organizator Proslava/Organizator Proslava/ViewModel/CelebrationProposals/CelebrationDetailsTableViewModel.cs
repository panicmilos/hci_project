using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Utility;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Organizator_Proslava.Services.Contracts;

namespace Organizator_Proslava.ViewModel.CelebrationProposals
{
    public class CelebrationDetailsTableViewModel
    {
        private CelebrationResponse _celebrationResponse { get; set; }
        public CelebrationResponse CelebrationResponse
        {
            get => _celebrationResponse;
            set
            {
                _celebrationResponse = value;
                CelebrationDetails =
                    new ObservableCollection<CelebrationDetail>(CelebrationResponse.Celebration.CelebrationDetails);
            }
        }

        public ObservableCollection<CelebrationDetail> CelebrationDetails { get; set; }
        private CelebrationDetail _currentCelebrationDetail;

        public ICommand Preview { get; set; }
        public ICommand Proposals { get; set; }
        public ICommand Add { get; set; }
        public ICommand Back { get; set; }

        private readonly IDialogService _dialogService;

        private readonly CelebrationProposalsTableViewModel _cptvm;

        public CelebrationDetailsTableViewModel(
            CelebrationProposalsTableViewModel cptvm,
            ICelebrationService celebrationService,
            ICrudService<CelebrationDetail> celebrationDetailService,
            IDialogService dialogService)
        {
            _cptvm = cptvm;
            _dialogService = dialogService;

            Preview = new RelayCommand<CelebrationDetail>(cd => _dialogService.OpenDialog(new CelebrationDetailDialogViewModel(cd)));
            
            Add = new RelayCommand(() =>
            {
                var celebrationDetail = _dialogService.OpenDialog(new AddCelebrationDetailViewModel(null, _dialogService));
                if (celebrationDetail == null) return;
                celebrationDetail.CelebrationId = CelebrationResponse.CelebrationId;
                var cd = celebrationDetailService.Create(celebrationDetail);
                CelebrationResponse.Celebration.CelebrationDetails.ToList().Add(cd);
                CelebrationDetails.Add(cd);
                // celebrationService.Update(CelebrationResponse.Celebration);
            });
            
            Proposals = new RelayCommand<CelebrationDetail>(cd =>
            {
                _currentCelebrationDetail = cd;
                _cptvm.CelebrationResponse = CelebrationResponse;
                _cptvm.CelebrationProposals = new ObservableCollection<CelebrationProposal>(CelebrationResponse.CelebrationProposalsDict[_currentCelebrationDetail]);
                EventBus.FireEvent("SwitchCelebrationProposalsViewModel", _cptvm);
            });

            EventBus.RegisterHandler("PreviewProposalsFromNotificationClient", cd => Proposals.Execute(cd));

            EventBus.RegisterHandler("BackToProposalsTableForClient", () => EventBus.FireEvent("SwitchCelebrationProposalsViewModel", _cptvm));

            Back = new RelayCommand(() => EventBus.FireEvent("BackToClientPage"));
        }
    }
}