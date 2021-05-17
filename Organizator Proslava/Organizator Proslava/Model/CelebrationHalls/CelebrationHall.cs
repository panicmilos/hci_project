using System.Collections.Generic;

namespace Organizator_Proslava.Model.CelebrationHalls
{
    public class CelebrationHall : BaseObservableEntity
    {
        private string _name;
        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }

        private int _numberOfGuests;
        public int NumberOfGuests { get => _numberOfGuests; set => OnPropertyChanged(ref _numberOfGuests, value); }

        private List<PlaceableEntity> _placeableEntities { get; set; }

        public virtual List<PlaceableEntity> PlaceableEntities
        {
            get { return _placeableEntities; }
            set { _placeableEntities = value; OnPropertyChanged("PlaceableEntities"); }
        }
    }
}