using Organizator_Proslava.Dialogs.Alert;
using Organizator_Proslava.Dialogs.Map;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.Cellebrations;
using Organizator_Proslava.Services.Contracts;
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
    public class CreateOrganizerViewModel
    {
        public Organizer Organizer { get; set; }
        public string CelebrationType { get; set; }
        public ICommand Create { get; set; }
        public ICommand Back { get; set; }
        public ICommand Map { get; set; }
        public ObservableCollection<string> CelebrationTypes { get; set; }

        private readonly IOrganizerService _organizerService;
        private readonly IDialogService _dialogService;
        private readonly ICelebrationTypeService _celebrationTypeService;

        public CreateOrganizerViewModel(IOrganizerService organizerService, IDialogService dialogService, ICelebrationTypeService celebrationTypeService)
        {
            Organizer = new Organizer
            {
                Address = new Address()
            };
            _organizerService = organizerService;
            _dialogService = dialogService;
            _celebrationTypeService = celebrationTypeService;
            CelebrationTypes = new ObservableCollection<string>(_celebrationTypeService.ReadNames());

            RegisterHandlerToEventBus();

            Back = new RelayCommand(() => EventBus.FireEvent("OrganizersTableView"));

            Map = new RelayCommand(() =>
            {
                var result = _dialogService.OpenDialog(new MapDialogViewModel("Odaberi lokaciju"));
                Organizer.Address = result;
            });
        }

        private void RegisterHandlerToEventBus()
        {
            EventBus.RegisterHandler("CreateOrganizer", ForAdd);
            EventBus.RegisterHandler("UpdateOrganizer", organizer => ForUpdate(organizer));
        }

        public void ForUpdate(object o)
        {
            Organizer = o as Organizer;
            CelebrationType = Organizer.CellebrationType.Name;

            Create = new RelayCommand<Organizer>(organizer =>
            {
                var optionDialogResult = _dialogService.OpenDialog(new OptionDialogViewModel("Potvrda", "Da li ste sigurni da želite da izmenite organizatora?"));
                if (optionDialogResult == Dialogs.DialogResults.Yes)
                {
                    organizer.CellebrationType = _celebrationTypeService.ReadByName(CelebrationType);
                    if (organizer.CellebrationType == null)
                    {
                        organizer.CellebrationType = new CellebrationType
                        {
                            Name = CelebrationType
                        };
                    }
                    _organizerService.Update(organizer);
                    EventBus.FireEvent("OrganizersTableView");
                    EventBus.FireEvent("ReloadOrganizerTable");
                    _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje", "Uspešno ste izmenili nalog."));
                }
            });
        }

        public void ForAdd()
        {
            Organizer = new Organizer();
            CelebrationType = "";
            Create = new RelayCommand<Organizer>(o =>
            {
                var optionDialogResult = _dialogService.OpenDialog(new OptionDialogViewModel("Potvrda", "Da li ste sigurni da želite da kreirate novog organizatora?"));
                if (optionDialogResult == Dialogs.DialogResults.Yes)
                {
                    o.Role = Role.Organizer;
                    o.CellebrationType = _celebrationTypeService.ReadByName(CelebrationType);
                    if (o.CellebrationType == null)
                    {
                        o.CellebrationType = new CellebrationType
                        {
                            Name = CelebrationType
                        };
                    }
                    _organizerService.Create(o);
                    EventBus.FireEvent("OrganizersTableView");
                    EventBus.FireEvent("ReloadOrganizerTable");
                    _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje", "Uspešno ste napravili nalog."));
                }
            });
        }
    }
}
