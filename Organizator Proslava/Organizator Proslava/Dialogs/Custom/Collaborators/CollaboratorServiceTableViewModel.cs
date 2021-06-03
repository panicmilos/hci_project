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
        public ICommand Edit { get; set; }
        public ICommand Remove { get; set; }
        public ObservableCollection<CollaboratorService> Services { get; set; }

        private readonly IDialogService _dialogService;

        private CollaboratorServiceBook _collaboratorServiceBook;
        public CollaboratorServiceBook CollaboratorServiceBook { get => _collaboratorServiceBook; set => OnPropertyChanged(ref _collaboratorServiceBook, value); }

        public CollaboratorServiceTableViewModel(Collaborator collaborator)
            : base("Usluge saradnika", 590, 450)
        {
            _dialogService = new DialogService();

            CollaboratorServiceBook = new CollaboratorServiceBook
            {
                Services = new List<CollaboratorService>()
            };

            Services = new ObservableCollection<CollaboratorService>(collaborator.CollaboratorServiceBook.Services);
            Back = new RelayCommand<IDialogWindow>(window => {
                CloseDialogWithResult(window, DialogResults.Undefined);

                if (collaborator is IndividualCollaborator)
                    _dialogService.OpenDialog(new IndividualCollaboratorDetailViewModel(collaborator as IndividualCollaborator));
                else
                    _dialogService.OpenDialog(new LegalCollaboratorDetailViewModel(collaborator as LegalCollaborator));
            });

            Edit = new RelayCommand<CollaboratorService>(service =>
            {
                var serviceCopy = service.Clone();
                var editedService = _dialogService.OpenDialog(new CollaboratorServiceDialogViewModel(serviceCopy));
                if (editedService != null)
                {
                    service.Name = editedService.Name;
                    service.Price = editedService.Price;
                    service.Unit = editedService.Unit;
                }
            });

            Remove = new RelayCommand<CollaboratorService>(service =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Pitanje", "Da li ste sigurni da želite da obrišete ovu uslugu?")) == DialogResults.Yes)
                {
                    Services.Remove(service);
                    CollaboratorServiceBook.Services.Remove(service);
                }
            });
        }
    }
}
