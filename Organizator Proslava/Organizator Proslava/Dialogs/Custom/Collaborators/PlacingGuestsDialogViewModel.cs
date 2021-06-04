using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel;
using System.Linq;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    public class PlacingGuestsDialogViewModel : DialogViewModelBase<DinningTable>
    {
        public DinningTable DinningTable { get; set; }
        public SpacePreviewMode Mode { get; set; }
        public bool ShouldShowSave { get => Mode == SpacePreviewMode.Edit; }

        public ICommand Add { get; set; }
        public ICommand AddNewGuest { get; set; }
        public ICommand Remove { get; set; }

        public ICommand Save { get; set; }
        public ICommand Back { get; set; }

        private readonly IDialogService _dialogService;

        public PlacingGuestsDialogViewModel(DinningTable dinningTable, IDialogService dialogService, SpacePreviewMode mode = SpacePreviewMode.View) :
            base("Gosti", 660, 460)
        {
            DinningTable = dinningTable;
            Mode = mode;
            _dialogService = dialogService;

            Add = new RelayCommand<Guest>(AddGuest);
            AddNewGuest = new RelayCommand(() => EventBus.FireEvent("AddNewGuest"), () => DinningTable.Guests.Count() < DinningTable.Seats);
            Remove = new RelayCommand<int>(RemoveGuest);

            GlobalStore.AddObject("guests", DinningTable.Guests);
            GlobalStore.AddObject("tableImageName", DinningTable.ImageName);
            GlobalStore.AddObject("spacePreviewMode", mode);

            Save = new RelayCommand<IDialogWindow>(w =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Pitanje", "Da li ste sigurni da želite da sačuvate ovaj raspored gostiju?")) == DialogResults.Yes)
                {
                    CloseDialogWithResult(w, DinningTable);
                }
            });
            Back = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, null));
        }

        private void AddGuest(Guest guest)
        {
            DinningTable.Guests.Add(guest);
        }

        private void RemoveGuest(int guestNo)
        {
            DinningTable.Guests.RemoveAt(guestNo);
        }
    }
}