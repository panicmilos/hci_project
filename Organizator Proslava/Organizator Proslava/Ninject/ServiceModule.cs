using Ninject.Modules;
using Organizator_Proslava.Data;
using Organizator_Proslava.Dialogs.Custom.Celebrations;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Services.Implementations;
using Organizator_Proslava.ViewModel;
using Organizator_Proslava.ViewModel.CelebrationProposals;

namespace Organizator_Proslava.Ninject
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(ICrudService<>)).To(typeof(CrudService<>));
            Bind(typeof(IUserService<>)).To(typeof(UserService<>));
            Bind(typeof(IClientService)).To(typeof(ClientService));
            Bind(typeof(ICelebrationTypeService)).To(typeof(CelebrationTypeService));
            Bind(typeof(IOrganizerService)).To(typeof(OrganizerService));
            Bind(typeof(ICollaboratorService)).To(typeof(CollaboratorCrudService));
            Bind(typeof(ICelebrationService)).To(typeof(CelebrationService));
            Bind(typeof(ICelebrationResponseService)).To(typeof(CelebrationResponseService));
            Bind(typeof(ICelebrationProposalService)).To(typeof(CelebrationProposalService));
            Bind(typeof(IProposalCommentService)).To(typeof(ProposalCommentService));
            Bind(typeof(INotificationService)).To(typeof(NotificationService));
            Bind(typeof(ICelebrationHallService)).To(typeof(CelebrationHallService));

            Bind(typeof(IDialogService)).To(typeof(DialogService));
            Bind(typeof(IDemoService)).To(typeof(DemoService));

            Bind<DatabaseContext>().To<DatabaseContext>().InSingletonScope();

            Bind<LoginViewModel>().To<LoginViewModel>();
            Bind<RegisterViewModel>().To<RegisterViewModel>();
        }
    }
}