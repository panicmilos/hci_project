using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CelebrationResponseForm
{
    public class ProposalsTableForOrganizerViewModel
    {
        public ObservableCollection<CelebrationProposal> CelebrationProposals { get; set; }

        public ICommand Preview { get; set; }
        public ICommand Comments { get; set; }
        public ICommand Add { get; set; }
        public ICommand Back { get; set; }

        private readonly IDialogService _dialogService;

        public ProposalsTableForOrganizerViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            Preview = new RelayCommand<CelebrationProposal>(cd => { });
            Comments = new RelayCommand<CelebrationProposal>(cd => { });
            Add = new RelayCommand(() =>
            {
                var proposal = _dialogService.OpenDialog(new CelebrationProposalDialogViewModel());
                if (proposal != null)
                {
                    CelebrationProposals.Add(proposal);
                }
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToRequestDetailsForOrganizer"));
        }
    }
}