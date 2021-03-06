using Organizator_Proslava.Dialogs.Map;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.Utils;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CelebrationRequestForm
{
    public class CelebrationRequestInfoViewModel : ObservableEntity, IDataErrorInfo
    {
        private Organizer _organizer;
        public Organizer Organizer { get => _organizer; set => OnPropertyChanged(ref _organizer, value); }

        private Celebration _celebration;
        public Celebration Celebration { get => _celebration; set => OnPropertyChanged(ref _celebration, value); }

        // Text fields:
        private string _celebrationType;

        public string CelebrationType { get => _celebrationType; set { _celebrationType = value; _celebration.Type = value; OnPropertyChanged("ShouldShowOrganizers"); } }
        public string ExpectedNumberOfGuests { get; set; }
        public string BudgetFrom { get; set; }
        public string BudgetTo { get; set; }
        public DateTime DateTimeFrom { get => _dateTimeFrom; set { OnPropertyChanged(ref _dateTimeFrom, value); OnPropertyChanged("DateTimeTo"); } }
        public DateTime DateTimeTo { get => _dateTimeTo; set { OnPropertyChanged(ref _dateTimeTo, value); OnPropertyChanged("DateTimeFrom"); } }

        private DateTime _dateTimeFrom = DateTime.Now, _dateTimeTo = DateTime.Now;

        // Commands:
        public ICommand OpenOrganizersDialog { get; set; }

        public ICommand OpenMap { get; set; }
        public ICommand Back { get; set; }
        public ICommand Next { get; set; }

        public ObservableCollection<string> CelebrationTypes { get; set; }
        public bool ShouldShowOrganizers { get => _organizerService.OrganizersExistFor(_celebration.Type); }

        // Rules:
        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                if (columnName == "DateTimeFrom" || columnName == "DateTimeTo")
                    return Err(ValidationDictionary.Validate(columnName, DateTimeFrom, DateTimeTo));

                var valueOfProperty = GetType().GetProperty(columnName)?.GetValue(this);
                return Err(ValidationDictionary.Validate(columnName, valueOfProperty, null));
            }
        }

        private readonly ICelebrationTypeService _celebrationTypeService;
        private readonly IOrganizerService _organizerService;
        private readonly IDialogService _dialogService;
        private int _calls = 0;

        public CelebrationRequestInfoViewModel(
            ICelebrationTypeService celebrationTypeService,
            ICelebrationService celebrationService,
            IOrganizerService organizerService,
            IDialogService dialogService)
        {
            _celebrationTypeService = celebrationTypeService;
            _organizerService = organizerService;
            _dialogService = dialogService;

            CelebrationTypes = new ObservableCollection<string>(_celebrationTypeService.ReadNames());

            OpenOrganizersDialog = new RelayCommand(() =>
            {
                var organizer = _dialogService.OpenDialog(new ChooseOrganizerViewModel(_celebration.Type, organizerService, celebrationService));
                if (organizer != null)
                {
                    Organizer = organizer;
                    Celebration.OrganizerId = Organizer.Id;
                }
            });
            OpenMap = new RelayCommand(() =>
            {
                var address = _dialogService.OpenDialog(new MapDialogViewModel("Odabir adrese proslave"));
                if (address != null)
                {
                    Celebration.Address = address;
                }
            });
            Back = new RelayCommand(() => EventBus.FireEvent("BackToClientPage"));
            Next = new RelayCommand(() =>
            {
                Celebration.Type = _celebrationType;
                Celebration.DateTimeFrom = DateTimeFrom;
                Celebration.DateTimeTo = DateTimeTo;
                Celebration.ExpectedNumberOfGuests = int.Parse(ExpectedNumberOfGuests);
                float budgetFrom = float.Parse(BudgetFrom), budgetTo = float.Parse(BudgetTo);
                Celebration.BudgetFrom = budgetFrom < budgetTo ? budgetFrom : budgetTo;
                Celebration.BudgetTo = budgetFrom < budgetTo ? budgetTo : budgetFrom;
                EventBus.FireEvent("NextToCelebrationRequestDetails");
            });
        }

        public void ForAdd()
        {
            Celebration = new Celebration();
            _celebrationType = string.Empty;
            DateTimeFrom = DateTime.Now;
            DateTimeTo = DateTime.Now;
            ExpectedNumberOfGuests = string.Empty;
            BudgetFrom = string.Empty;
            BudgetTo = string.Empty;
            _calls = 0;
        }

        public void ForUpdate(Celebration celebration)
        {
            _calls = 6;
            Celebration = celebration;
            _celebrationType = Celebration.Type;
            DateTimeFrom = Celebration.DateTimeFrom;
            DateTimeTo = Celebration.DateTimeTo;
            ExpectedNumberOfGuests = Celebration.ExpectedNumberOfGuests.ToString();
            BudgetFrom = Celebration.BudgetFrom.ToString();
            BudgetTo = Celebration.BudgetTo.ToString();
        }

        private string Err(string message)
        {
            return message == null ? null : (_calls++ < 6 ? "*" : message);   // there are 7 fields
        }
    }
}