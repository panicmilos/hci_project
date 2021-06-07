using Organizator_Proslava.Dialogs.Alert;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.Utils;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    public class SpaceModelingViewModel : DialogViewModelBase<CelebrationHall>, IDataErrorInfo
    {
        public CelebrationHall Hall { get; set; }
        private string _name;
        public string Name { get { return _name; } set { OnPropertyChanged(ref _name, value); Hall.Name = value; } }

        public int NumberOfGuests { get => Hall.NumberOfGuests; }

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

        // Dialog things
        public ICommand Save { get; set; }

        public ICommand Back { get; set; }

        private readonly IDialogService _dialogService;

        public SpaceModelingViewModel() :
            this(new CelebrationHall())
        {
        }

        public SpaceModelingViewModel(CelebrationHall celebrationHall) :
            base("Modelovanje prostora", 800, 550)
        {
            // Modeling
            Hall = celebrationHall;
            Name = celebrationHall.Name;

            Add = new RelayCommand<PlaceableEntity>(AddEntity);
            Remove = new RelayCommand<int>(RemoveEntity);
            GlobalStore.AddObject("placeableEntities", Hall.PlaceableEntities);

            // Dialog
            _dialogService = new DialogService();

            Save = new RelayCommand<IDialogWindow>(w =>
            {
                if (!Hall.PlaceableEntities.Any())
                {
                    _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje", "Morate postaviti bar jedan sto."));
                    return;
                }
                CloseDialogWithResult(w, Hall);
            });

            Back = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, null));
        }

        public void AddEntity(PlaceableEntity placeableEntity)
        {
            Hall.PlaceableEntities.Add(placeableEntity);
            OnPropertyChanged("NumberOfGuests");
        }

        public void RemoveEntity(int entityNo)
        {
            Hall.PlaceableEntities.RemoveAt(entityNo);
            OnPropertyChanged("NumberOfGuests");
        }

        private string Err(string message)
        {
            return message == null ? null : (_calls++ < 1 ? "*" : message);   // there are 7 fields
        }
    }
}