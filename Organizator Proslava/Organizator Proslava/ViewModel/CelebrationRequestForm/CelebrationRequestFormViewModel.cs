using Organizator_Proslava.Data;
using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel.CelebrationRequestForm
{
    public class CelebrationRequestFormViewModel : NavigabileModelView
    {
        private readonly DatabaseContext _context;
        
        public CelebrationRequestFormViewModel(DatabaseContext context)
        {
            _context = context;
        }

        public void RegisterEventBusHandlers()
        {
        }
    }
}