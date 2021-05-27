using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CelebrationResponseForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class OrganizerHomeViewModel
    {
        public ICommand Preview { get; set; }
        public ICommand Cancel { get; set; }
        public ICommand Back { get; set; }

        public IEnumerable<CelebrationResponse> CelebrationResponses { get; set; } = new List<CelebrationResponse>
        {
            new CelebrationResponse
            {
                Celebration = new Celebration
                {
                    Type = "Rodjendan",
                    Client = new Client
                    {
                        FirstName = "Milos", LastName = "Panic"
                    },
                    Organizer = new Organizer
                    {
                        FirstName = "Milan",
                        LastName = "Susic"
                    },
                    DateTimeFrom = DateTime.Now,
                    Address = new Address
                    {
                        WholeAddress = "Neka adresica veca ipak malo 123",
                        Lat = 56f,
                        Lng = 24f
                    },
                    IsActive = true,
                    BudgetFrom = 5000,
                    BudgetTo = 10000,
                    IsBudgetFixed = true,
                    CelebrationDetails = new List<CelebrationDetail>()
                    {
                        new CelebrationDetail
                        {
                            Title = "Neki zahtev 1",
                            Content = "kjlasfjkfsajklfsakjjkasfjklasfjklafsjkfasjklasfjklafsjklasfjklajfksljklafs"
                        },

                        new CelebrationDetail
                        {
                            Title = "Neki zahtev 3",
                            Content = "kjlasfjkfsajklfsakjjkasfjklasfjklafsjkfasjklasfjklafsjklasfjklajfksljklafs"
                        },

                        new CelebrationDetail
                        {
                            Title = "Neki zahtev 2",
                            Content = "kjlasfjkfsajklfsakjjkasfjklasfjklafsjkfasjklasfjklafsjklasfjklajfksljklafs"
                        },
                    },
                    DateTimeTo = DateTime.Now.AddDays(2),
                    ExpectedNumberOfGuests = 100
                }
            }
        };

        private readonly CelebrationResponseFormViewModel _crfvm;

        public OrganizerHomeViewModel(CelebrationResponseFormViewModel crfvm)
        {
            CelebrationResponses.ToList()[0].CelebrationProposals = new List<CelebrationProposal>()
            {
                new CelebrationProposal
                {
                    CelebrationDetail = CelebrationResponses.ToList()[0].Celebration.CelebrationDetails.ToList()[0],
                    Title = "jaksdkajsdjkasd",
                    Content = "sadkljasdklasdkl"
                },
                new CelebrationProposal
                {
                    CelebrationDetail = CelebrationResponses.ToList()[0].Celebration.CelebrationDetails.ToList()[0],
                    Title = "asgasg",
                    Content = "asgasg"
                },

                 new CelebrationProposal
                {
                    CelebrationDetail = CelebrationResponses.ToList()[0].Celebration.CelebrationDetails.ToList()[1],
                    Title = "gasgasgaga",
                    Content = "gasgasgas"
                },
                new CelebrationProposal
                {
                    CelebrationDetail = CelebrationResponses.ToList()[0].Celebration.CelebrationDetails.ToList()[1],
                    Title = "yeryeryeryery",
                    Content = "yerreyerery"
                },
            };

            _crfvm = crfvm;

            Preview = new RelayCommand<CelebrationResponse>(cr =>
            {
                EventBus.FireEvent("ShowCelebrationRequest", cr);
                EventBus.FireEvent("SwitchMainViewModel", _crfvm);
            });

            Cancel = new RelayCommand(() =>
                new DialogService().OpenDialog(new DialogWindow(),
                    new OptionDialogViewModel("Potvrda otkazivanja proslave",
                        "Da li ste sigurni da želite da otkažete organizovanje proslave?")));

            Back = new RelayCommand(() => EventBus.FireEvent("BackToLogin"));
        }
    }
}