using System.Collections.Generic;
using System.Windows.Input;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel.CelebrationRequestForm
{
    public class ChooseOrganizerViewModel : DialogViewModelBase<Organizer>
    {
        public Organizer SelectedOrganizer { get; set; }
        
        public ICommand Choose { get; set; }
        public ICommand Cancel { get; set; }
        
        public IEnumerable<Organizer> Organizers { get; set; } = new List<Organizer>
        {
            new Organizer
            {
                FirstName = "Milos",
                LastName = "Panic",
                PhoneNumber = "012301023012",
            },
            new Organizer
            {
                FirstName = "Luka",
                LastName = "Bjelica",
                PhoneNumber = "22222023012",
            },
            new Organizer
            {
                FirstName = "Jox",
                LastName = "Jevtic",
                PhoneNumber = "333301023012",
            },
        };
        
        public ChooseOrganizerViewModel() : base("Izaberi organizatora", 560, 360)
        {
            Choose = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, SelectedOrganizer));
            Cancel = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, null));
        }
    }
}