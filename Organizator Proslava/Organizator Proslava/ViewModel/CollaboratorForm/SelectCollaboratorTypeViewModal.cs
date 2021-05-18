using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CollaboratorForm
{
    public class SelectCollaboratorTypeViewModal
    {
        public ICommand Back { get; set; }
        public ICommand IndividualSelected { get; set; }
        public ICommand LegalSelected { get; set; }

        public SelectCollaboratorTypeViewModal()
        {
            Back = new RelayCommand(() => EventBus.FireEvent("BackToLogin"));

            IndividualSelected = new RelayCommand(() => EventBus.FireEvent("IndividualSelected"));

            LegalSelected = new RelayCommand(() => EventBus.FireEvent("LegalSelected"));
        }
    }
}