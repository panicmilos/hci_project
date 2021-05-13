namespace Organizator_Proslava.Model
{
    public class Address : BaseObservableEntity
    {
        private string _wholeAddress;
        public string WholeAddress { get => _wholeAddress; set => OnPropertyChanged(ref _wholeAddress, value); }

        private float _lat;
        public float Lat { get => _lat; set => OnPropertyChanged(ref _lat, value); }

        private float _lng;
        public float Lng { get => _lng; set => OnPropertyChanged(ref _lng, value); }
    }
}