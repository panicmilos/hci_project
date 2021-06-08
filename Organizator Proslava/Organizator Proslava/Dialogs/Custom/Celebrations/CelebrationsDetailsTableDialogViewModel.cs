using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Celebrations
{
    public class CelebrationsDetailsTableDialogViewModel : DialogViewModelBase<DialogResults>
    {
        public ObservableCollection<CelebrationDetail> CelebrationDetails { get; set; }

        public ICommand Preview { get; set; }
        public ICommand Proposals { get; set; }
        public ICommand Back { get; set; }

        private readonly IDialogService _dialogService;
        private readonly ICelebrationResponseService _celebrationResponseService;

        public Celebration Celebration { get; set; }

        public CelebrationsDetailsTableDialogViewModel(IDialogService dialogService, ICelebrationResponseService celebrationResponseService)
        {
            _dialogService = dialogService;
            _celebrationResponseService = celebrationResponseService;

            Celebration = new Celebration();

            //CelebrationDetails = new ObservableCollection<CelebrationDetail>(Celebration.CelebrationDetails);

            Back = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, DialogResults.Undefined));

            Preview = new RelayCommand<CelebrationDetail>(celebrationDetail =>
            {
                EventBus.FireEvent("CelebrationDetailPreview", celebrationDetail);
            });

            Proposals = new RelayCommand<CelebrationDetail>(celebrationDetail =>
            {
                var celebrationResponse = _celebrationResponseService.ReadForCelebration(Celebration.Id);
                ObservableCollection<CelebrationProposal> proposals = new ObservableCollection<CelebrationProposal>(celebrationResponse.CelebrationProposalsDict[celebrationDetail]);
                EventBus.FireEvent("CelebrationProposals", proposals);
            });
        }
    }
}
