using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    public class NonDinningTableDialogViewModel : DialogViewModelBase<PlaceableEntity>
    {
        public PlaceableEntity Entity { get; set; }

        public string ButtonText { get; set; }

        public ObservableCollection<string> MoveableOptions { get; set; } = new ObservableCollection<string>()
        {
            "Da",
            "Ne"
        };

        public ICommand Add { get; set; }
        public ICommand Back { get; set; }

        public NonDinningTableDialogViewModel(PlaceableEntity entity, bool isEditing) :
            base("Dodavanje stola", 560, 160)
        {
            Entity = entity;
            ButtonText = isEditing ? "Sačuvaj" : "Dodaj";

            Add = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, Entity));
            Back = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, null));
        }
    }
}