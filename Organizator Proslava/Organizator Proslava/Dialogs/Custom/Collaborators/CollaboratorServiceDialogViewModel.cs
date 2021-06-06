using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.Utils;
using System.ComponentModel;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    public class CollaboratorServiceDialogViewModel : DialogViewModelBase<CollaboratorService>, IDataErrorInfo
    {
        public CollaboratorService Service { get; set; }

        private string _name;
        public string Name { get { return _name; } set { OnPropertyChanged(ref _name, value); Service.Name = value; } }

        private string _price;
        public string Price { get { return _price; } set { OnPropertyChanged(ref _price, value); if (float.TryParse(value, out var price)) Service.Price = price; } }

        private string _unit;
        public string Unit { get { return _unit; } set { OnPropertyChanged(ref _unit, value); Service.Unit = value; } }

        // Validation
        public string this[string columnName]
        {
            get
            {
                var valueOfProperty = GetType().GetProperty(columnName)?.GetValue(this);
                return Err(ValidationDictionary.Validate("CS" + columnName, valueOfProperty, null));
            }
        }

        public string ButtonText { get; set; } = "Sačuvaj";
        private int _calls = 0;

        public ICommand Add { get; set; }
        public ICommand Back { get; set; }

        public string Error => throw new System.NotImplementedException();

        public CollaboratorServiceDialogViewModel(CollaboratorService service) :
            base("Dodavanje usloge", 560, 360)
        {
            Service = service;
            Name = service.Name;
            Price = service.Price.ToString();
            Unit = service.Unit;

            Add = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, Service));
            Back = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, null));
        }

        public CollaboratorServiceDialogViewModel() :
            this(new CollaboratorService())
        {
            ButtonText = "Dodaj";
        }

        private string Err(string message)
        {
            return message == null ? null : (_calls++ < 3 ? "*" : message);   // there are 7 fields
        }
    }
}