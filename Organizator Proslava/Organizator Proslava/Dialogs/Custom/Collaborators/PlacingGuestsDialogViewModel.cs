using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using System.Diagnostics;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    public class PlacingGuestsDialogViewModel : DialogViewModelBase<DinningTable>
    {
        public DinningTable DinningTable { get; set; }

        public ICommand Add { get; set; }
        public ICommand Remove { get; set; }

        public ICommand Save { get; set; }
        public ICommand Back { get; set; }

        public PlacingGuestsDialogViewModel(DinningTable dinningTable) :
            base("Gosti", 660, 460)
        {
            DinningTable = dinningTable;

            Add = new RelayCommand<Guest>(AddGuest);
            Remove = new RelayCommand<int>(RemoveGuest);
            DinningTable.Guests.Clear();
            DinningTable.Guests.Add(new Guest
            {
                Name = "Milos Panic",
                PositionX = 55,
                PositionY = 66
            });
            DinningTable.Guests.Add(new Guest
            {
                Name = " Zoran Jankov",
                PositionX = 300,
                PositionY = 320
            });
            DinningTable.Guests.Add(new Guest
            {
                Name = "Klimenta Jovana",
                PositionX = 125,
                PositionY = 125
            });
            GlobalStore.AddObject("guests", DinningTable.Guests);
            GlobalStore.AddObject("tableImageName", DinningTable.ImageName);

            Save = new RelayCommand<IDialogWindow>(w =>
            {
                foreach (var guest in DinningTable.Guests)
                {
                    Trace.Write(guest.Name);
                    Trace.WriteLine("   " + guest.PositionX + " " + guest.PositionY);
                }
                CloseDialogWithResult(w, DinningTable);
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