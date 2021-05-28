using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CelebrationResponseForm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.OrganizatorHome
{
    public class CurrentOrganizatorCelebrationsTableViewModel
    {
        public ICommand NonAcceptedCelebrations { get; set; }

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

        public CurrentOrganizatorCelebrationsTableViewModel(ICrudService<Celebration> crudService, ICrudService<Client> clients, CelebrationResponseFormViewModel crfvm)
        {
            NonAcceptedCelebrations = new RelayCommand(() => EventBus.FireEvent("NextToAcceptCelebrationRequestTable"));
            crudService.Create(new Celebration
            {
                BudgetFrom = 2000,
                BudgetTo = 6000,
                IsBudgetFixed = true,
                CelebrationDetails = new List<CelebrationDetail>
                {
                    new CelebrationDetail
                    {
                        Title = "Zahtev 1",
                        Content = "Text zahteva koji sada treba da bude kao nesto dugggg"
                    },
                    new CelebrationDetail
                    {
                        Title = "Zahtev 2",
                        Content = "text zahteva kao nesto 2",
                    }
                },
                Client = clients.Read(new Guid("08d92223-3a29-4112-8707-4c38205264d0")),
                DateTimeFrom = DateTime.Now,
                DateTimeTo = DateTime.Now.AddDays(2),
                ExpectedNumberOfGuests = 200,
                Type = "Rodjendan"
            });

            CelebrationResponses.ToList()[0].CelebrationProposals = new List<CelebrationProposal>()
            {
                new CelebrationProposal
                {
                    CelebrationDetail = CelebrationResponses.ToList()[0].Celebration.CelebrationDetails.ToList()[0],
                    Title = "jaksdkajsdjkasd",
                    Content = "sadkljasdklasdkl",
                    ProposalComments = new List<ProposalComment>
                    {
                        new ProposalComment
                        {
                            Content = "akjlsfjkasfjklasfjkl,.;safklsalkasfl;ksaflk;sflak;kl;safkl;faskl;asfkl;asfkl;asfkl;safkl;asfkl;asf;klasfk;lasfk;lfsak;lasfk;lsafkl;asfl;kafskl;asfl;kasfk;lasflk;asfkl;asfkl;asfk;lasfk;lasfkl;kals;fkl;asfkl;asfkl;saflk;asflk;saflk;safkl;saflk;asfl;kalks;fkl;asf;klasfkl;safkl;saf;klsaf;lksafkl;safl;ksaf;klsaf;lk;klsafkl;saf;klsafkl;asfl;kfas;lkasfkl;safl;kk;lsafk;lfl;kafskl;afslk;afskl;afskl;asfkl;lkas;fkl;afsk;lasfk;lasfk;lasf;lkasf;klasfkl;asf;klk;lasfk;lasfk;lsafkl;asfkl;safkl;asfkl;kl;afskl;asfkl;afskl;asfl;kasfk;lasfk;lafsk;lasfjklafskljasfkljljkafsljkafsljkasfljkasfljkas",
                            Writer = new Client
                            {
                                Id = new Guid("9eeafd35-9850-4663-baf7-491f92ae84f5"),
                                FirstName = "Jelisaveta",
                                LastName = "Papic"
                            }
                        },
                        new ProposalComment
                        {
                            Content = "ioasiopasdipoasdpioasdiopsapodiiopasd",
                            Writer = new Client
                            {
                                Id = new Guid("e0c1f7b7-1d73-43fd-98cc-c904295afb62"),
                                FirstName = "Milos",
                                LastName = "Panic"
                            }
                        },
                        new ProposalComment
                        {
                            Content = "251125gdasagssagagsashassdhgqweheq",
                            Writer = new Client
                            {
                                Id = new Guid("9eeafd35-9850-4663-baf7-491f92ae84f5"),
                                FirstName = "Jelisaveta",
                                LastName = "Papic"
                            }
                        }
                    }
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
                EventBus.FireEvent("SwitchOrganizerViewModel", _crfvm);
            });

            Cancel = new RelayCommand(() =>
                new DialogService().OpenDialog(new DialogWindow(),
                    new OptionDialogViewModel("Potvrda otkazivanja proslave",
                        "Da li ste sigurni da želite da otkažete organizovanje proslave?")));

            Back = new RelayCommand(() => EventBus.FireEvent("BackToLogin"));
        }
    }
}