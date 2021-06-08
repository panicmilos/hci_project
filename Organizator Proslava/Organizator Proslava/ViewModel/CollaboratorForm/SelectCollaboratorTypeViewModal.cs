using Organizator_Proslava.UserCommands;
using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CollaboratorForm
{
    public class SelectCollaboratorTypeViewModal
    {
        public string BackTo { get; set; }

        public ICommand Back { get; set; }
        public ICommand IndividualSelected { get; set; }
        public ICommand LegalSelected { get; set; }

        public SelectCollaboratorTypeViewModal()
        {
            Back = new RelayCommand(() => { EventBus.FireEvent(BackTo); GlobalStore.ReadObject<IUserCommandManager>("userCommands").Clear(); });

            IndividualSelected = new RelayCommand(() => EventBus.FireEvent("IndividualSelected"));

            LegalSelected = new RelayCommand(() => EventBus.FireEvent("LegalSelected"));
        }
    }
}