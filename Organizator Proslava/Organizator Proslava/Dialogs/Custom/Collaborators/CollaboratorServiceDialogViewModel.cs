using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    public class CollaboratorServiceDialogViewModel : DialogViewModelBase<CollaboratorService>
    {
        public CollaboratorService Service { get; set; }

        public string ButtonText { get; set; } = "Sačuvaj";

        public ICommand Add { get; set; }
        public ICommand Back { get; set; }

        public CollaboratorServiceDialogViewModel(CollaboratorService service) :
            base("Dodavanje usloge", 560, 360)
        {
            Service = service;

            Add = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, Service));
            Back = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, null));
        }

        public CollaboratorServiceDialogViewModel() :
            this(new CollaboratorService())
        {
            ButtonText = "Dodaj";
        }
    }
}