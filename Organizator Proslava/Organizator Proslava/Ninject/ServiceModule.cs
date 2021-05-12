using Ninject.Modules;
using Organizator_Proslava.Data;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Services.Implementations;
using Organizator_Proslava.ViewModel;

namespace Organizator_Proslava.Ninject
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind(typeof(ICrudService<>)).To(typeof(CrudService<>));
            Bind(typeof(IUserService<>)).To(typeof(UserService<>));
            Bind(typeof(IClientService)).To(typeof(ClientService));
            Bind(typeof(IDialogService)).To(typeof(DialogService));

            Bind<DatabaseContext>().To<DatabaseContext>();

            Bind<LoginViewModel>().To<LoginViewModel>();
            Bind<RegisterViewModel>().To<RegisterViewModel>();
        }
    }
}