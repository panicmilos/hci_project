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
        private Celebration _celebration;
        public Celebration Celebration { get => _celebration; set => OnPropertyChanged(ref _celebration, value); }
        // Text fields:
        private string _celebrationType;
        public string CelebrationType { get => _celebrationType; set { _celebrationType = value; OnPropertyChanged("ShouldShowOrganizers"); } }
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
                if (columnName == "DateTimeFrom"|| columnName == "DateTimeTo")
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
            IOrganizerService organizerService,
            IDialogService dialogService)
        {
            _celebrationTypeService = celebrationTypeService;
            _organizerService = organizerService;
            _dialogService = dialogService;

            CelebrationTypes = new ObservableCollection<string>(_celebrationTypeService.ReadNames());

            OpenOrganizersDialog = new RelayCommand(() =>
            {
                Celebration.Organizer = _dialogService.OpenDialog(new ChooseOrganizerViewModel(organizerService));
            });
            OpenMap = new RelayCommand(() =>
            {
                var address = _dialogService.OpenDialog(new MapDialogViewModel("Odaberite adresu proslave"));
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
                Celebration.BudgetFrom = float.Parse(BudgetFrom);
                Celebration.BudgetTo = float.Parse(BudgetTo);
                EventBus.FireEvent("NextToCelebrationRequestDetails");
            });
        }

        public void ForAdd()
        {
            Celebration = new Celebration();
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