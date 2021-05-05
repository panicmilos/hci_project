using Ninject;
using Organizator_Proslava.ViewModel;

namespace Organizator_Proslava.Ninject
{
    public class ServiceLocator
    {
        private readonly IKernel _kernel;

        public ServiceLocator()
        {
            _kernel = new StandardKernel(new ServiceModule());
        }

        public MainViewModel MainViewModel
        {
            get => _kernel.Get<MainViewModel>();
        }
    }
}