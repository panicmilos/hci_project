using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CelebrationResponseForm;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.OrganizatorHome
{
    public class CurrentOrganizatorCelebrationsTableViewModel
    {
        public ObservableCollection<CelebrationResponse> CelebrationResponses { get; set; }

        public ICommand NonAcceptedCelebrations { get; set; }

        public ICommand Preview { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand Back { get; set; }

        private readonly ICelebrationResponseService _celebrationResponseService;

        private readonly CelebrationResponseFormViewModel _crfvm;

        public CurrentOrganizatorCelebrationsTableViewModel(ICelebrationResponseService celebrationResponseService, CelebrationResponseFormViewModel crfvm)
        {
            _celebrationResponseService = celebrationResponseService;

            NonAcceptedCelebrations = new RelayCommand(() => EventBus.FireEvent("NextToAcceptCelebrationRequestTable"));

            _crfvm = crfvm;

            Preview = new RelayCommand<CelebrationResponse>(cr =>
            {
                EventBus.FireEvent("ShowCelebrationRequest", cr);
                EventBus.FireEvent("SwitchOrganizerViewModel", _crfvm);
            });

            EventBus.RegisterHandler("PreviewResponseForNotification", cr => Preview.Execute(cr));

            Cancel = new RelayCommand(() =>
                new DialogService().OpenDialog(new OptionDialogViewModel("Potvrda otkazivanja proslave",
                        "Da li ste sigurni da želite da otkažete organizovanje proslave?")));

            Back = new RelayCommand(() => EventBus.FireEvent("BackToLogin"));

            Reload();
        }

        public void Reload()
        {
            var loggedUser = GlobalStore.ReadObject<BaseUser>("loggedUser");
            CelebrationResponses = new ObservableCollection<CelebrationResponse>(_celebrationResponseService.ReadOrganizingBy(loggedUser.Id));
        }
    }
}