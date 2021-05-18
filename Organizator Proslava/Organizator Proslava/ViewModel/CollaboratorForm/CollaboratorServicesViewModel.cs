using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Custom.Collaborators;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.Cellebrations;
using Organizator_Proslava.Model.Collaborators;
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

namespace Organizator_Proslava.ViewModel.CollaboratorForm
{
    public class CollaboratorServicesViewModel : ObservableEntity
    {
        public string CameFrom { get; set; }

        private ICelebrationTypeService _celebrationTypeService;
        public ObservableCollection<string> CelebrationTypes { get; set; }

        private CollaboratorServiceBook _collaboratorServiceBook;
        public CollaboratorServiceBook CollaboratorServiceBook { get => _collaboratorServiceBook; set => OnPropertyChanged(ref _collaboratorServiceBook, value); }

        public ObservableCollection<CollaboratorService> Services { get; set; }

        private string _inputedCelebrationDescription;
        public string InputedCelebrationDescription { get => _inputedCelebrationDescription; set => OnPropertyChanged(ref _inputedCelebrationDescription, value); }

        private IDialogService _dialogService;

        public ICommand Back { get; set; }
        public ICommand Next { get; set; }

        public ICommand Add { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Remove { get; set; }

        public CollaboratorServicesViewModel(ICelebrationTypeService celebrationTypeService, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _celebrationTypeService = celebrationTypeService;

            CelebrationTypes = new ObservableCollection<string>(_celebrationTypeService.ReadNames());
            CollaboratorServiceBook = new CollaboratorServiceBook();
            CollaboratorServiceBook.Services = new List<CollaboratorService>()
            {
                new CollaboratorService
                {
                    Name = "Piletina",
                    Price = 300f,
                    Unit = "Porcija"
                },
                new CollaboratorService
                {
                    Name = "Teletina",
                    Price = 500f,
                    Unit = "Kila"
                },
            };

            Services = new ObservableCollection<CollaboratorService>(CollaboratorServiceBook.Services);

            Add = new RelayCommand(() =>
            {
                var service = dialogService.OpenDialog(new CollaboratorServiceDialogViewModel());
                if (service != null)
                {
                    Services.Add(service);
                    CollaboratorServiceBook.Services.Add(service);
                }
            });

            Edit = new RelayCommand<CollaboratorService>(service =>
            {
                var serviceCopy = service.Copy();
                var editedService = dialogService.OpenDialog(new CollaboratorServiceDialogViewModel(serviceCopy));
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

            Back = new RelayCommand(() => EventBus.FireEvent("BackToCollaboratorInformations", CameFrom));
        }
    }
}