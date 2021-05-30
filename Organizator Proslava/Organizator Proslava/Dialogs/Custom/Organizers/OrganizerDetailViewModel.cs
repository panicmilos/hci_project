using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Services.Implementations;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Organizers
{
    public class OrganizerDetailViewModel : DialogViewModelBase<DialogResults>
    {
        public Organizer Organizer { get; set; }
        public ICommand Back { get; set; }

        public OrganizerDetailViewModel(Organizer organizer):
            base("Pregled organizatora", 590, 420)
        {
            Organizer = organizer;
            Back = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, DialogResults.Undefined));
        }
    }
}
