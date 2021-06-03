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
    public class IndividualCollaboratorDetailViewModel : DialogViewModelBase<DialogResults>
    {
        public IndividualCollaborator Collaborator { get; set; }
        public ICommand Back { get; set; }
        
        public IndividualCollaboratorDetailViewModel(IndividualCollaborator collaborator):
            base("Pregled saradnika", 590, 420)
        {
            Collaborator = collaborator;
            Back = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, DialogResults.Undefined));
        }
    }
}
