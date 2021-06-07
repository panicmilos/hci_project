using System.Collections.Generic;

namespace Organizator_Proslava.Model.CelebrationHalls
{
    public abstract class DinningTable : PlaceableEntity
    {
        private int _seats = 1;
        public int Seats { get => _seats; set => OnPropertyChanged(ref _seats, value); }

        private List<Guest> _guests;

        public virtual List<Guest> Guests
        {
            get { return _guests; }
            set { _guests = value; OnPropertyChanged("Guests"); }
        }

        public DinningTable()
        {
            Guests = new List<Guest>();
        }
    }
}