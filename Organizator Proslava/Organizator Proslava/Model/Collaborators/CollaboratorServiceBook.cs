using System.Collections.Generic;

namespace Organizator_Proslava.Model.Collaborators
{
    public class CollaboratorServiceBook : BaseObservableEntity
    {
        private string _type;
        public string Type { get => _type; set => OnPropertyChanged(ref _type, value); }

        private string _description;
        public string Description { get => _description; set => OnPropertyChanged(ref _description, value); }

        private List<CollaboratorService> _services { get; set; }

        public virtual List<CollaboratorService> Services
        {
            get { return _services; }
            set { _services = value; OnPropertyChanged("Services"); }
        }
    }
}