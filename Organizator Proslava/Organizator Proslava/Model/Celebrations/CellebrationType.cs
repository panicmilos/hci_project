namespace Organizator_Proslava.Model.Cellebrations
{
    public class CellebrationType : BaseObservableEntity, ICloneable<CellebrationType>
    {
        private string _name;
        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }

        public CellebrationType Clone()
        {
            return new CellebrationType
            {
                Id = Id,
                IsActive = IsActive,
                CreatedAt = CreatedAt,
                Name = Name
            };
        }
    }
}