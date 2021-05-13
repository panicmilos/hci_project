namespace Organizator_Proslava.Model.CelebrationHalls
{
    public class PlaceableEntity : BaseObservableEntity
    {
        private double _positionX;
        public double PositionX { get => _positionX; set => OnPropertyChanged(ref _positionX, value); }

        private double _positionY;
        public double PositionY { get => _positionY; set => OnPropertyChanged(ref _positionY, value); }

        private PlaceableEntityType _type;
        public PlaceableEntityType Type { get => _type; set => OnPropertyChanged(ref _type, value); }
    }
}