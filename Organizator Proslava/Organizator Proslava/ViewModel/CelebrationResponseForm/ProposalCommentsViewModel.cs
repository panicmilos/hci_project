using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Utility;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CelebrationResponseForm
{
    public class ProposalCommentsViewModel : ObservableEntity
    {
        public CelebrationProposal CelebrationProposal { get; set; }
        public ObservableCollection<ProposalComment> ProposalComments { get; set; }

        public ICommand Back { get; set; }
        public ICommand Comment { get; set; }

        private readonly IDialogService _dialogService;

        public ProposalCommentsViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            Back = new RelayCommand(() => EventBus.FireEvent("BackToProposalsTableForOrganizer"));
            Comment = new RelayCommand(() =>
            {
                var commentText = _dialogService.OpenDialog(new WriteCommentDialogViewModel());
                if (commentText != null)
                {
                    var loggedUser = GlobalStore.ReadObject<BaseUser>("loggedUser");
                    var comment = new ProposalComment()
                    {
                        Writer = loggedUser,
                        CelebrationProposal = CelebrationProposal,
                        Content = commentText
                    };

                    CelebrationProposal.ProposalComments.Add(comment);
                    ProposalComments.Add(comment);
                }
            });
        }
    }
}