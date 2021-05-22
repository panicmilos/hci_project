using System;

namespace Organizator_Proslava.Model.Collaborators
{
    public class CollaboratorService : BaseObservableEntity
    {
        private string _name;
        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }

        private float _price;
        public float Price { get => _price; set => OnPropertyChanged(ref _price, value); }

        private string _unit;
        public string Unit { get => _unit; set => OnPropertyChanged(ref _unit, value); }

        public Guid CollaboratorServiceBookId { get; set; }
        public CollaboratorServiceBook CollaboratorServiceBook { get; set; }
    }
}