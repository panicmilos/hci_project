using Ninject;
using Organizator_Proslava.ViewModel;

namespace Organizator_Proslava.Ninject
{
    public class ServiceLocator
    {
        private static IKernel _kernel = new StandardKernel(new ServiceModule());

        public static T Get<T>() => _kernel.Get<T>();

        public MainViewModel MainViewModel
        {
            get => _kernel.Get<MainViewModel>();
        }

        public static void Reset()
        {
            _kernel = new StandardKernel(new ServiceModule());
        }
    }
}