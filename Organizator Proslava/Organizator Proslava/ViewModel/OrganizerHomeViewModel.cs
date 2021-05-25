using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel
{
    public class OrganizerHomeViewModel
    {
        public ICommand Cancel { get; set; }
        public ICommand Back { get; set; }
        public IEnumerable<Celebration> Celebrations { get; set; } = new List<Celebration>
        {
            new Celebration
            {
                Type = "Rodjendan",
                Organizer = new Organizer
                {
                    FirstName = "Milan",
                    LastName = "Susic"
                },
                DateTimeFrom = DateTime.Now
            }
        };
        
        public OrganizerHomeViewModel()
        {
            Cancel = new RelayCommand(() =>
                new DialogService().OpenDialog(new DialogWindow(),
                    new OptionDialogViewModel("Potvrda otkazivanja proslave",
                        "Da li ste sigurni da želite da otkažete organizovanje proslave?")));
            
            Back = new RelayCommand(() => EventBus.FireEvent("BackToLogin"));
        }
    }
}