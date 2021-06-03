using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    public class CollaboratorServiceTableViewModel : DialogViewModelBase<DialogResults>
    {
        public ICommand Back { get; set; }
        public ObservableCollection<CollaboratorService> Services { get; set; }

        private readonly IDialogService _dialogService;

        public CollaboratorServiceTableViewModel(Collaborator collaborator)
            : base("Usluge saradnika", 590, 450)
        {
            _dialogService = new DialogService();

            Services = new ObservableCollection<CollaboratorService>(collaborator.CollaboratorServiceBook.Services);
            Back = new RelayCommand<IDialogWindow>(window => {
                CloseDialogWithResult(window, DialogResults.Undefined);

                if (collaborator is IndividualCollaborator)
                    _dialogService.OpenDialog(new IndividualCollaboratorDetailViewModel(collaborator as IndividualCollaborator));
                else
                    _dialogService.OpenDialog(new LegalCollaboratorDetailViewModel(collaborator as LegalCollaborator));
            });
        }
    }
}
