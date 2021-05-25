﻿using Organizator_Proslava.Dialogs.Alert;
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
        public string celebrationType { get; set; }
        public ICommand Create { get; set; }
        public ICommand Back { get; set; }
        public ICommand Map { get; set; }
        public ObservableCollection<string> CelebrationTypes { get; set; }

        private readonly IOrganizerService _organizerService;
        private readonly IDialogService _dialogService;
        private ICelebrationTypeService _celebrationTypeService;

        public CreateOrganizerViewModel(IOrganizerService organizerService, IDialogService dialogService, ICelebrationTypeService celebrationTypeService)
        {
            Organizer = new Organizer
            {
                Address = new Address(),
                CellebrationType = new CellebrationType()
            };
            _organizerService = organizerService;
            _dialogService = dialogService;
            _celebrationTypeService = celebrationTypeService;
            CelebrationTypes = new ObservableCollection<string>(_celebrationTypeService.ReadNames());

            Create = new RelayCommand<Organizer>(o =>
            {
                var optionDialogResult = _dialogService.OpenDialog(new OptionDialogViewModel("Potvrda", "Da li ste sigurni da želite da kreirate novog organizatora?"));
                if (optionDialogResult == Dialogs.DialogResults.Yes)
                {
                    o.Role = Role.Organizer;
                    //o.CellebrationType = _celebrationTypeService.ReadByName(o.CellebrationType.Name);
                    o.CellebrationType = new CellebrationType
                        {
                            Name = celebrationType
                    };
                    _organizerService.Create(o);
                    EventBus.FireEvent("BackToLogin");
                    _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje", "Uspešno ste napravili nalog."));
                }
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToLogin"));

            Map = new RelayCommand(() =>
            {
                var result = new DialogService().OpenDialog(new MapDialogViewModel("Odaberi lokaciju"));
                Organizer.Address = result;
                var chosen = result == null ? "Nista" : $"{result.WholeAddress} ${result.Lat} ${result.Lng}";
                new DialogService().OpenDialog(new AlertDialogViewModel("Izabrao si", chosen));
            }); // Delete Later
        }
    }
}