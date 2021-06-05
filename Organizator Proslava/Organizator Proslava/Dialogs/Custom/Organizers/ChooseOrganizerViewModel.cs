using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Organizator_Proslava.Services.Contracts;

namespace Organizator_Proslava.ViewModel.CelebrationRequestForm
{
    public class ChooseOrganizerViewModel : DialogViewModelBase<Organizer>
    {
        public Organizer SelectedOrganizer { get; set; }

        public ICommand Choose { get; set; }
        public ICommand Cancel { get; set; }

        public ObservableCollection<Organizer> Organizers { get; set; }

        public ChooseOrganizerViewModel(IOrganizerService organizerService) : base("Izaberi organizatora", 560, 360)
        {
            Organizers = new ObservableCollection<Organizer>(organizerService.Read());
            
            Choose = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, SelectedOrganizer));
            Cancel = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, null));
        }
    }
}