using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using System.Collections.Generic;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CelebrationResponseForm
{
    public class RequestDetailsTableForOrganizerViewModel
    {
        public List<CelebrationDetail> CelebrationDetails { get; set; }

        public ICommand Preview { get; set; }
        public ICommand Back { get; set; }

        private IDialogService _dialogService;

        public RequestDetailsTableForOrganizerViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            Preview = new RelayCommand<CelebrationDetail>(cd => _dialogService.OpenDialog(new CelebrationDetailDialogViewModel(cd)));
            Back = new RelayCommand(() => EventBus.FireEvent("BackToRequestLongPreviewForOranizer"));
        }
    }
}