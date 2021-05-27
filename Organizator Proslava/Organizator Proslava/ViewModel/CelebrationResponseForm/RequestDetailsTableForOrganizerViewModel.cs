using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Utility;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CelebrationResponseForm
{
    public class RequestDetailsTableForOrganizerViewModel
    {
        public CelebrationResponse CelebrationResponse { get; set; }

        public ICommand Preview { get; set; }
        public ICommand Proposals { get; set; }
        public ICommand Back { get; set; }

        private readonly IDialogService _dialogService;
        private readonly ProposalsTableForOrganizerViewModel _ptfovm;

        public RequestDetailsTableForOrganizerViewModel(ProposalsTableForOrganizerViewModel ptfovm, IDialogService dialogService)
        {
            _ptfovm = ptfovm;
            _dialogService = dialogService;

            Preview = new RelayCommand<CelebrationDetail>(cd => _dialogService.OpenDialog(new CelebrationDetailDialogViewModel(cd)));
            Proposals = new RelayCommand<CelebrationDetail>(cd =>
            {
                _ptfovm.CelebrationProposals = new ObservableCollection<CelebrationProposal>(CelebrationResponse.CelebrationProposalsDict[cd]);
                EventBus.FireEvent("SwitchCelebrationResponseFormViewModel", _ptfovm);
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToRequestLongPreviewForOranizer"));
        }
    }
}