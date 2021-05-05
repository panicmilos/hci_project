using Organizator_Proslava.Utility;

namespace Organizator_Proslava.Model
{
    /// <summary>
    /// This class exists only so that this folder will be pushed on git.
    /// Remove this class when this folder have at least one real class.
    /// </summary>
    public class DummyClass : ObservableEntity
    {
        private string _name;
        public string Name { get => _name; set => OnPropertyChanged(ref _name, value); }

        private string _surname;
        public string Surname { get => _surname; set => OnPropertyChanged(ref _surname, value); }
    }
}