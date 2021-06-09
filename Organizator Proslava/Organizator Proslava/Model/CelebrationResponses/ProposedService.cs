namespace Organizator_Proslava.Model.CelebrationResponses
{
    public class ProposedService : BaseObservableEntity
    {
        private string _name;
        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }

        private float _price;
        public float Price { get => _price; set => OnPropertyChanged(ref _price, value); }

        private string _unit;
        public string Unit { get => _unit; set => OnPropertyChanged(ref _unit, value); }

        private int _numberOfService;
        public int NumberOfService { get => _numberOfService; set => OnPropertyChanged(ref _numberOfService, value); }

        public override string ToString()
        {
            return $"{Unit} x {Name} po ceni od {Price}RSD/{Unit}";
        }
    }
}