using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Celebrations
{
    public class CelebrationProposalDialogViewModel : DialogViewModelBase<CelebrationProposal>
    {
        public CelebrationProposal Proposal { get; set; }

        public ICommand Add { get; set; }
        public ICommand Back { get; set; }

        public CelebrationProposalDialogViewModel() : base("Davanje ponude", 500, 660)
        {
            Proposal = new CelebrationProposal();

            Add = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, Proposal));
            Back = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, null));
        }
    }
}