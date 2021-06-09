using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    public enum SpacePreviewMode
    {
        View,
        Edit
    }

    public class SpacePreviewDialogViewModel : DialogViewModelBase<CelebrationHall>
    {
        public CelebrationHall Hall { get; set; }
        public SpacePreviewMode Mode { get; set; }

        public bool ShouldShowSave { get => Mode == SpacePreviewMode.Edit; }

        public ICommand Save { get; set; }
        public ICommand Back { get; set; }

        private readonly IDialogService _dialogService;

        public SpacePreviewDialogViewModel(CelebrationHall celebrationHall, IDialogService dialogService, SpacePreviewMode mode = SpacePreviewMode.View) :
            base("Pregled prostora", 650, 550)
        {
            _dialogService = dialogService;
            Hall = celebrationHall;
            Mode = mode;

            GlobalStore.AddObject("placeableEntities", Hall.PlaceableEntities);
            GlobalStore.AddObject("spacePreviewMode", mode);

            Save = new RelayCommand<IDialogWindow>(w =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Potvrda", "Da li ste sigurni da želite da sačuvate ovaj raspored sale?")) == DialogResults.Yes)
                {
                    CloseDialogWithResult(w, Hall);
                }
            });

            Back = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, null));
        }

        public void ChangeBack()
        {
            Back = new RelayCommand<IDialogWindow>(w => EventBus.FireEvent("BackToHallsTable"));
        }
    }
}