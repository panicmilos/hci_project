using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Alert;
using Organizator_Proslava.Dialogs.Custom.Collaborators;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.UserCommands;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CollaboratorForm
{
    public class CollaboratorServicesViewModel : ObservableEntity, IDataErrorInfo
    {
        public string CameFrom { get; set; }

        private readonly ICelebrationTypeService _celebrationTypeService;
        public ObservableCollection<string> CelebrationTypes { get; set; }

        private CollaboratorServiceBook _collaboratorServiceBook;
        public CollaboratorServiceBook CollaboratorServiceBook { get => _collaboratorServiceBook; set => OnPropertyChanged(ref _collaboratorServiceBook, value); }

        public ObservableCollection<CollaboratorService> Services { get; set; }

        // For TextBoxes:

        private string _type;
        public string Type { get { return _type; } set { OnPropertyChanged(ref _type, value); CollaboratorServiceBook.Type = value; } }

        private string _description;
        public string Description { get { return _description; } set { OnPropertyChanged(ref _description, value); CollaboratorServiceBook.Description = value; } }

        // Validation
        public string this[string columnName]
        {
            get
            {
                var valueOfProperty = GetType().GetProperty(columnName)?.GetValue(this);
                return Err(ValidationDictionary.Validate("CS" + columnName, valueOfProperty, null));
            }
        }

        private readonly IDialogService _dialogService;
        private int _calls = 0;

        public ICommand Back { get; set; }
        public ICommand Next { get; set; }

        public ICommand Add { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Remove { get; set; }

        public string Error => throw new NotImplementedException();

        public CollaboratorServicesViewModel(ICelebrationTypeService celebrationTypeService, IDialogService dialogService)
        {
            _dialogService = dialogService;
            _celebrationTypeService = celebrationTypeService;

            CelebrationTypes = new ObservableCollection<string>(_celebrationTypeService.ReadNames());
            CollaboratorServiceBook = new CollaboratorServiceBook
            {
                Services = new List<CollaboratorService>()
            };
            Services = new ObservableCollection<CollaboratorService>(CollaboratorServiceBook.Services);

            Add = new RelayCommand(() =>
            {
                var service = dialogService.OpenDialog(new CollaboratorServiceDialogViewModel());
                if (service != null)
                {
                    Services.Add(service);
                    CollaboratorServiceBook.Services.Add(service);

                    GlobalStore.ReadObject<IUserCommandManager>("userCommands").Add(new CreateService(service, Services, CollaboratorServiceBook));
                }
            });

            Edit = new RelayCommand<CollaboratorService>(service =>
            {
                var serviceCopy = service.Clone();
                var editedService = dialogService.OpenDialog(new CollaboratorServiceDialogViewModel(serviceCopy));
                if (editedService != null)
                {
                    var currentServiceCopy = service.Clone();
                    var newServiceCopy = editedService.Clone();

                    GlobalStore.ReadObject<IUserCommandManager>("userCommands").Add(new UpdateService(service, currentServiceCopy, newServiceCopy));

                    service.Name = editedService.Name;
                    service.Price = editedService.Price;
                    service.Unit = editedService.Unit;
                }
            });

            Remove = new RelayCommand<CollaboratorService>(service =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Potvrda", "Da li ste sigurni da želite da obrišete ovu uslugu?")) == DialogResults.Yes)
                {
                    Services.Remove(service);
                    CollaboratorServiceBook.Services.Remove(service);

                    GlobalStore.ReadObject<IUserCommandManager>("userCommands").Add(new DeleteService(service, Services, CollaboratorServiceBook));
                }
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToCollaboratorInformations", CameFrom));
            Next = new RelayCommand(() =>
            {
                if (!CollaboratorServiceBook.Services.Any())
                {
                    _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje", "Saradnik mora imati bar jednu uslugu."));
                    return;
                }
                EventBus.FireEvent("NextToCollaboratorImages");
            });
        }

        public void ForAdd()
        {
            CollaboratorServiceBook = new CollaboratorServiceBook();
            Type = string.Empty;
            Description = string.Empty;
            _calls = 0;

            Services = new ObservableCollection<CollaboratorService>();
        }

        public void ForUpdate(Collaborator collaborator)
        {
            CollaboratorServiceBook = collaborator.CollaboratorServiceBook;
            Type = collaborator.CollaboratorServiceBook.Type;
            Description = collaborator.CollaboratorServiceBook.Description;
            _calls = 2;

            Services = new ObservableCollection<CollaboratorService>(collaborator.CollaboratorServiceBook.Services);
        }

        private string Err(string message)
        {
            return message == null ? null : (_calls++ < 2 ? "*" : message);   // there are 7 fields
        }
    }
}