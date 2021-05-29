using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.OrganizatorHome
{
    public class AcceptCelebrationRequestTableViewModel
    {
        public ObservableCollection<Celebration> CelebrationRequests { get; set; }

        public ICommand Preview { get; set; }
        public ICommand Accept { get; set; }

        public ICommand Back { get; set; }

        private readonly ICelebrationService _celebrationService;
        private readonly IDialogService _dialogService;

        public AcceptCelebrationRequestTableViewModel(ICelebrationService celebrationService, IDialogService dialogService)
        {
            _celebrationService = celebrationService;
            _dialogService = dialogService;

            Reload();

            Preview = new RelayCommand<Celebration>(c => _dialogService.OpenDialog(new CelebrationLongPreviewDialogViewModel(c)));
            Accept = new RelayCommand<Celebration>(c =>
            {
                var result = _dialogService.OpenDialog(new OptionDialogViewModel("Pitanje", "Da li ste sigurni da želite da prihvatite ovu organizaciju?"));
                if (result == DialogResults.Yes)
                {
                    var loggerOrganizerId = GlobalStore.ReadObject<BaseUser>("loggedUser").Id;
                    celebrationService.AcceptBy(loggerOrganizerId, c.Id);

                    CelebrationRequests.Remove(c);
                }
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToCurrentCelebrationsForOrganizer"));
        }

        public void Reload()
        {
            CelebrationRequests = new ObservableCollection<Celebration>(_celebrationService.ReadNotTaken());
        }
    }
}