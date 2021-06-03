using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    public class LegalCollaboratorDetailViewModel : DialogViewModelBase<DialogResults>
    {
        public ICommand Back { get; set; }
        public ICommand Services { get; set; }
        public ICommand Images { get; set; }
        public ICommand Halls { get; set; }
        public LegalCollaborator Collaborator { get; set; }

        private readonly IDialogService _dialogService;

        public LegalCollaboratorDetailViewModel(LegalCollaborator collaborator):
            base("Pregled saradnika", 590, 420)
        {
            _dialogService = new DialogService();
            Collaborator = collaborator;
            Back = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, DialogResults.Undefined));
            Images = new RelayCommand<IDialogWindow>(window => {
                CloseDialogWithResult(window, DialogResults.Undefined);
                _dialogService.OpenDialog(new DisplayImagesViewModel(collaborator));
             });
        }
    }
}
