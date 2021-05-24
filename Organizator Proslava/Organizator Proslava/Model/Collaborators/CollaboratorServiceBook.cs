using System;
using System.Collections.Generic;
using System.Linq;

namespace Organizator_Proslava.Model.Collaborators
{
    public class CollaboratorServiceBook : BaseObservableEntity, ICloneable<CollaboratorServiceBook>
    {
        private string _type;
        public string Type { get => _type; set => OnPropertyChanged(ref _type, value); }

        private string _description;
        public string Description { get => _description; set => OnPropertyChanged(ref _description, value); }

        private List<CollaboratorService> _services;

        public virtual List<CollaboratorService> Services
        {
            get { return _services; }
            set { _services = value; OnPropertyChanged("Services"); }
        }

        public Guid CollaboratorId { get; set; }

        public CollaboratorServiceBook()
        {
            Services = new List<CollaboratorService>();
        }

        public CollaboratorServiceBook Clone()
        {
            return new CollaboratorServiceBook
            {
                Id = Id,
                CreatedAt = CreatedAt,
                IsActive = IsActive,
                CollaboratorId = CollaboratorId,
                Description = Description,
                Type = Type,
                Services = new List<CollaboratorService>(Services.Select(s => s.Clone()))
            };
        }
    }
}