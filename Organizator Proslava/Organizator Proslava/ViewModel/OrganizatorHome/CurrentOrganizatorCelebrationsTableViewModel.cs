using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CelebrationResponseForm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        public CurrentOrganizatorCelebrationsTableViewModel(ICelebrationResponseService celebrationResponseService, ICrudService<Celebration> crudService, ICrudService<Client> clients, CelebrationResponseFormViewModel crfvm)
        {
            _celebrationResponseService = celebrationResponseService;

            NonAcceptedCelebrations = new RelayCommand(() => EventBus.FireEvent("NextToAcceptCelebrationRequestTable"));

            //crudService.Create(new Celebration
            //{
            //    BudgetFrom = 2000,
            //    BudgetTo = 6000,
            //    IsBudgetFixed = true,
            //    CelebrationDetails = new List<CelebrationDetail>
            //    {
            //        new CelebrationDetail
            //        {
            //            Title = "Zahtev 1",
            //            Content = "Text zahteva koji sada treba da bude kao nesto dugggg"
            //        },
            //        new CelebrationDetail
            //        {
            //            Title = "Zahtev 2",
            //            Content = "text zahteva kao nesto 2",
            //        }
            //    },
            //    Client = clients.Read(new Guid("08d92223-3a29-4112-8707-4c38205264d0")),
            //    DateTimeFrom = DateTime.Now,
            //    DateTimeTo = DateTime.Now.AddDays(2),
            //    ExpectedNumberOfGuests = 200,
            //    Type = "Rodjendan"
            //});

            _crfvm = crfvm;

            Preview = new RelayCommand<CelebrationResponse>(cr =>
            {
                EventBus.FireEvent("ShowCelebrationRequest", cr);
                EventBus.FireEvent("SwitchOrganizerViewModel", _crfvm);
            });

            Cancel = new RelayCommand(() =>
                new DialogService().OpenDialog(new DialogWindow(),
                    new OptionDialogViewModel("Potvrda otkazivanja proslave",
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