using Organizator_Proslava.Model.Collaborators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Organizator_Proslava.Model.CelebrationHalls
{
    public class CelebrationHall : BaseObservableEntity, ICloneable<CelebrationHall>
    {
        private string _name;
        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }

        public int NumberOfGuests { get => PlaceableEntities.Sum(pe => (pe as DinningTable)?.Seats ?? 0); }

        private List<PlaceableEntity> _placeableEntities;

        public virtual List<PlaceableEntity> PlaceableEntities
        {
            get { return _placeableEntities; }
            set { _placeableEntities = value; OnPropertyChanged("PlaceableEntities"); OnPropertyChanged("NumberOfGuests"); }
        }

        private Guid? _collaboratorId;
        public Guid? CollaboratorId { get => _collaboratorId; set => OnPropertyChanged(ref _collaboratorId, value); }

        private Collaborator _collaborator;
        public virtual Collaborator Collaborator { get => _collaborator; set => OnPropertyChanged(ref _collaborator, value); }

        public CelebrationHall()
        {
            PlaceableEntities = new List<PlaceableEntity>();
        }

        public override string ToString()
        {
            return Name;
        }

        public CelebrationHall Clone()
        {
            return new CelebrationHall
            {
                Id = Id,
                IsActive = IsActive,
                CreatedAt = CreatedAt,
                CollaboratorId = CollaboratorId,
                Name = Name,
                PlaceableEntities = new List<PlaceableEntity>(PlaceableEntities.Select(pe => pe.Clone()))
            };
        }
    }
}