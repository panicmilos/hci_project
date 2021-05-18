namespace Organizator_Proslava.Model.Cellebrations
{
    public class CellebrationType : BaseObservableEntity
    {
        private string _name;
        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }
    }
}