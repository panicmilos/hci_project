using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Utility;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Organizator_Proslava.Model.Collaborators;
using System.Collections.Generic;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    public class ChooseCollaboratorViewModel : DialogViewModelBase<Collaborator>
    {
        public Collaborator SelectedCollaborator { get; set; }

        public ICommand Choose { get; set; }
        public ICommand Cancel { get; set; }

        public ObservableCollection<Collaborator> Collaborators { get; set; }

        public ChooseCollaboratorViewModel(IEnumerable<Collaborator> collaborators) : base("Izaberi saradnika", 560, 360)
        {
            Collaborators = new ObservableCollection<Collaborator>(collaborators);

            Choose = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, SelectedCollaborator));
            Cancel = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, null));
        }
    }
}