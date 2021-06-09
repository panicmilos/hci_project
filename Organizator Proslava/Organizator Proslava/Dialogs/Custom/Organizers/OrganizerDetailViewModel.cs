using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Organizers
{
    public class OrganizerDetailViewModel : DialogViewModelBase<DialogResults>
    {
        public Organizer Organizer { get; set; }
        public ICommand Back { get; set; }

        public OrganizerDetailViewModel(Organizer organizer) :
            base("Pregled organizatora", 590, 440)
        {
            Organizer = organizer;
            Back = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, DialogResults.Undefined));
        }
    }
}