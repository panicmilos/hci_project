using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Custom.Organizers;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Services.Implementations;
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
    public class OrganziersTableViewModel
    {
        public ObservableCollection<Organizer> Organizers { get; set; }

        public ICommand Back { get; set; }
        public ICommand Add { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Remove { get; set; }
        public ICommand Details { get; set; }

        private readonly CreateOrganizerViewModel _covm;
        private readonly IOrganizerService _organizerService;
        private readonly IDialogService _dialogService;

        public OrganziersTableViewModel(CreateOrganizerViewModel covm, IOrganizerService organizerService, IDialogService dialogService)
        {
            _covm = covm;
            _organizerService = organizerService;
            _dialogService = dialogService;

            Organizers = new ObservableCollection<Organizer>(_organizerService.Read());

            Add = new RelayCommand(() =>
            { 
                EventBus.FireEvent("SwitchMainViewModel", _covm);
                _covm.ForAdd();
            });

            Edit = new RelayCommand<Organizer>(organizer =>
            {
                var organizerCopy = organizer.Clone();
                EventBus.FireEvent("SwitchMainViewModel", _covm);
                _covm.ForUpdate(organizerCopy);
            });

            Remove = new RelayCommand<Organizer>(organizer =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Pitanje", "Da li ste sigurni da želite da obrišete ovog organizatora?")) == DialogResults.Yes)
                {
                    Organizers.Remove(organizer);
                    _organizerService.Delete(organizer.Id);
                }
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToLogin"));

            Details = new RelayCommand<Organizer>(organizer =>
            {
                _dialogService.OpenDialog(new OrganizerDetailViewModel(organizer));
            });

            EventBus.RegisterHandler("ReloadOrganizerTable", () => Organizers = new ObservableCollection<Organizer>(_organizerService.Read()));
        }
    }
}
