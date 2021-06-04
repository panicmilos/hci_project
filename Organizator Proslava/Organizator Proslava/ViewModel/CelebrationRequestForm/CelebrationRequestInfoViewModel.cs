using System.Collections.ObjectModel;
using System.Windows.Input;
using Organizator_Proslava.Dialogs.Map;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel.CelebrationRequestForm
{
    public class CelebrationRequestInfoViewModel : ObservableEntity
    {
        private Celebration _celebration;
        public Celebration Celebration
        {
            get => _celebration;
            set => OnPropertyChanged(ref _celebration, value);
        }

        private string _selectedCelebrationType;
        public string SelectedCelebrationType
        {
            get => _selectedCelebrationType;
            set
            {
                _selectedCelebrationType = value;
                _celebration.Type = value;
                OnPropertyChanged("ShouldShowOrganizers");
            }
        }

        public ObservableCollection<string> CelebrationTypes { get; set; }
        public bool ShouldShowOrganizers { get => _organizerService.OrganizersExistFor(_celebration.Type); }
        
        public ICommand OpenOrganizersDialog { get; set; }
        public ICommand OpenMap { get; set; }
        public ICommand Back { get; set; }
        public ICommand Next { get; set; }
        
        private readonly ICelebrationTypeService _celebrationTypeService;
        private readonly IOrganizerService _organizerService;
        private readonly IDialogService _dialogService;

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
                Celebration.Address = _dialogService.OpenDialog(new MapDialogViewModel("Odaberite adresu proslave"));
            });
            Back = new RelayCommand(() => EventBus.FireEvent("BackToClientPage"));
            Next = new RelayCommand(() => EventBus.FireEvent("NextToCelebrationRequestDetails"));
        }

        public void ForAdd()
        {
            Celebration = new Celebration();
        }
        
        public void ForUpdate(Celebration celebration)
        {
            Celebration = celebration;
        }
    }
}