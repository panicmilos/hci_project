using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class CollaboratorsTableViewModel : ObservableEntity
    {
        public ObservableCollection<Collaborator> Collaborators { get; set; }

        public ICommand Back { get; set; }
        public ICommand Next { get; set; }

        public ICommand Add { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Remove { get; set; }

        private IDialogService _dialogService;

        public CollaboratorsTableViewModel()
        {
            Collaborators = new ObservableCollection<Collaborator>()
            {
                new IndividualCollaborator {
                    Address = new Address
                    {
                        WholeAddress = "NEKA ADRESA",
                    },
                    FirstName = "Milos",
                    LastName = "Panic",
                    MailAddress = "Panic.milos99@gmail.com",
                    JMBG = "2506123123123",
                    PhoneNumber = "012455215125",
                    Role = Role.Collaborator,
                    UserName = "PANICKO",
                    Password = "panic123",
                    PersonalId = "12412412124"
                },
                new LegalCollaborator
                {
                    Address = new Address
                    {
                        WholeAddress = "Pitaj boga"
                    },
                    FirstName = "Luka",
                    LastName = "Bjelica",
                    MailAddress = "bjelica.luka.neznmamdalje@gmail.com",
                    PIB = "214512512515",
                    IdentificationNumber = "1521616126612",
                    Password = "1412421214",
                    UserName = "LUKICABRE",
                    PhoneNumber = "124125125125",
                    Role = Role.Collaborator
                }
            };

            Add = new RelayCommand(() =>
            {
                //var service = dialogService.OpenDialog(new CollaboratorServiceDialogViewModel());
                //if (service != null)
                //{
                //    Services.Add(service);
                //    CollaboratorServiceBook.Services.Add(service);
                //}
            });

            Edit = new RelayCommand<Collaborator>(collaborator =>
            {
                //var serviceCopy = service.Copy();
                //var editedService = dialogService.OpenDialog(new CollaboratorServiceDialogViewModel(serviceCopy));
                //if (editedService != null)
                //{
                //    service.Name = editedService.Name;
                //    service.Price = editedService.Price;
                //    service.Unit = editedService.Unit;
                //}
            });

            Remove = new RelayCommand<Collaborator>(collaborator =>
            {
                //if (_dialogService.OpenDialog(new OptionDialogViewModel("Pitanje", "Da li ste sigurni da želite da obrišete ovu uslugu?")) == DialogResults.Yes)
                //{
                //    Services.Remove(service);
                //    CollaboratorServiceBook.Services.Remove(service);
                //}
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToLogin"));
        }
    }
}