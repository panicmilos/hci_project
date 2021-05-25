using Organizator_Proslava.Data;
using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel.CelebrationRequestForm
{
    public class CelebrationRequestFormViewModel : NavigabileModelView
    {
        public CelebrationRequestInfoViewModel Crivm;
        
        private readonly DatabaseContext _context;
        
        public CelebrationRequestFormViewModel(
            CelebrationRequestInfoViewModel crivm,
            DatabaseContext context)
        {
            Crivm = crivm;
            _context = context;
            
            Switch(Crivm);
            
            RegisterEventBusHandlers();
        }

        public void RegisterEventBusHandlers()
        {
        }
    }
}