using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CelebrationRequestForm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Services.Contracts;

namespace Organizator_Proslava.ViewModel
{
    public class ClientHomeViewModel
    {
        public ICommand Cancel { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Details { get; set; }
        public ICommand Back { get; set; }
        public ICommand Add { get; set; }
    
        public ObservableCollection<Celebration> Celebrations { get; set; }

        private readonly CelebrationRequestFormViewModel _crfvm;

        private readonly ICelebrationService _celebrationService;

        public ClientHomeViewModel(
            ICelebrationService celebrationService,
            CelebrationRequestFormViewModel crfvm,
            IDialogService dialogService)
        {
            _crfvm = crfvm;
            _celebrationService = celebrationService;
            
            LoadCelebrations();

            Details = new RelayCommand<Celebration>(celebration =>
            {
                dialogService.OpenDialog(new CelebrationLongPreviewDialogViewModel(celebration));
            });
            
            Cancel = new RelayCommand<Celebration>(celebration =>
            {
                if (dialogService.OpenDialog(new OptionDialogViewModel("Potvrda otkazivanja proslave",
                    "Da li ste sigurni da želite da otkažete proslavu?")) == DialogResults.No) return;
                Celebrations.Remove(Celebrations.FirstOrDefault(c => c.Id == celebration.Id));
                _celebrationService.Delete(celebration.Id);
            });
            
            Edit = new RelayCommand<Celebration>(celebration =>
            {
                EventBus.FireEvent("CelebrationRequestFromForUpdate", celebration);
                EventBus.FireEvent("SwitchMainViewModel", _crfvm);
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToLogin"));
            Add = new RelayCommand(() =>
            {
                EventBus.FireEvent("CelebrationRequestFormForAdd");
                EventBus.FireEvent("SwitchMainViewModel", _crfvm);
            });
            
            EventBus.RegisterHandler("CelebrationAddSuccess", LoadCelebrations);
            EventBus.RegisterHandler("CelebrationUpdateSuccess", LoadCelebrations);
        }

        private void LoadCelebrations()
        {
            Celebrations = new ObservableCollection<Celebration>(_celebrationService.ReadForClient(GlobalStore.ReadObject<BaseUser>("loggedUser").Id));
        }
    }
}