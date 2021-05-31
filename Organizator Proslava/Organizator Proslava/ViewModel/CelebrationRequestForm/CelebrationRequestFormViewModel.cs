using System;
using Organizator_Proslava.Data;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel.CelebrationRequestForm
{
    public class CelebrationRequestFormViewModel : NavigabileModelView
    {
        public CelebrationRequestInfoViewModel Crivm;
        public CelebrationRequestDetailsViewModel Crdvm;
        
        private readonly DatabaseContext _context;
        
        public CelebrationRequestFormViewModel(
            CelebrationRequestInfoViewModel crivm,
            CelebrationRequestDetailsViewModel crdvm,
            DatabaseContext context)
        {
            Crivm = crivm;
            Crdvm = crdvm;
            _context = context;
            
            Switch(Crivm);
            
            RegisterEventBusHandlers();
        }

        public void RegisterEventBusHandlers()
        {
            EventBus.RegisterHandler("BackToCelebrationRequestInfo", () => Switch(Crivm));
            EventBus.RegisterHandler("NextToAddCelebrationRequestDetails", ForAdd);
        }

        public void ForAdd()
        {
            Switch(Crdvm);
            Crdvm.ForAdd();
        }
        
        public void ForUpdate(Celebration celebration)
        {
            Switch(Crdvm);
            Crdvm.ForUpdate(celebration.CelebrationDetails);
        }
    }
}