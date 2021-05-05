using Ninject.Modules;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Theme;

namespace Organizator_Proslava.Ninject
{
    public class ServiceModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IDummyService>().To<DummyService>();
        }
    }
}