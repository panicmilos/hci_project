using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.Utils;
using System.ComponentModel;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class SpaceViewModel : BaseObservableEntity, IDataErrorInfo
    {
        public CelebrationHall Hall { get; set; }

        private string _name;
        public string Name { get { return _name; } set { OnPropertyChanged(ref _name, value); Hall.Name = value; } }

        private string _numberOfGuests;
        public string NumberOfGuests { get { return _numberOfGuests; } set { OnPropertyChanged(ref _numberOfGuests, value); if (int.TryParse(value, out int numberOfGuests)) Hall.NumberOfGuests = numberOfGuests; } }

        // Validation
        public string this[string columnName]
        {
            get
            {
                var valueOfProperty = GetType().GetProperty(columnName)?.GetValue(this);
                return Err(ValidationDictionary.Validate("CH" + columnName, valueOfProperty, null));
            }
        }

        private int _calls = 0;

        public ICommand Add { get; set; }
        public ICommand Remove { get; set; }

        public string Error => throw new System.NotImplementedException();

        public SpaceViewModel() :
            this(new CelebrationHall())
        {
        }

        public SpaceViewModel(CelebrationHall celebrationHall)
        {
            Hall = celebrationHall;
            Name = celebrationHall.Name;
            NumberOfGuests = celebrationHall.NumberOfGuests.ToString();

            Add = new RelayCommand<PlaceableEntity>(AddEntity);
            Remove = new RelayCommand<int>(RemoveEntity);
            GlobalStore.AddObject("placeableEntities", Hall.PlaceableEntities);
        }

        public void AddEntity(PlaceableEntity placeableEntity)
        {
            Hall.PlaceableEntities.Add(placeableEntity);
        }

        public void RemoveEntity(int entityNo)
        {
            Hall.PlaceableEntities.RemoveAt(entityNo);
        }

        private string Err(string message)
        {
            return message == null ? null : (_calls++ < 1 ? "*" : message);   // there are 7 fields
        }
    }
}