using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.Utils;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    public class DinningTableDialogViewModel : DialogViewModelBase<DinningTable>, IDataErrorInfo
    {
        public DinningTable Table { get; set; }

        private string _seats;

        public string Seats { get { return _seats; } set { OnPropertyChanged(ref _seats, value); if (int.TryParse(value, out var seats)) Table.Seats = seats; } }

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

        public string ButtonText { get; set; }

        public ObservableCollection<string> MoveableOptions { get; set; } = new ObservableCollection<string>()
        {
            "Da",
            "Ne"
        };

        public ICommand Add { get; set; }
        public ICommand Back { get; set; }

        public string Error => throw new System.NotImplementedException();

        public DinningTableDialogViewModel(DinningTable table, bool isEditing) :
            base($"{(!isEditing ? "Dodavanje" : "Izmena")} stola", 560, 260)
        {
            Table = table;
            Seats = table.Seats.ToString();

            ButtonText = isEditing ? "Sačuvaj" : "Dodaj";

            Add = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, Table));
            Back = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, null));
        }

        private string Err(string message)
        {
            return message == null ? null : (_calls++ < 0 ? "*" : message);   // there are 7 fields
        }
    }
}