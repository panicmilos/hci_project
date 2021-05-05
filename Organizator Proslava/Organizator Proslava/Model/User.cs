using Organizator_Proslava.Utility;

namespace Organizator_Proslava.Model
{
    public class User : BaseObservableEntity
    {
        private string _name;
        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }

        private string _surname;
        public string Surname { get => _surname; set => OnPropertyChanged(ref _surname, value); }
    }
}