using System;
using Organizator_Proslava.Data;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel.CelebrationRequestForm
{
    public class CelebrationRequestFormViewModel : NavigabileModelView
    {
        public CelebrationRequestInfoViewModel Crivm;
        public CelebrationRequestDetailsViewModel Crdvm;
        public CelebrationRequestPreviewViewModel Crpvm;

        private readonly ICelebrationService _celebrationService;
        private readonly DatabaseContext _context;
        
        public CelebrationRequestFormViewModel(
            CelebrationRequestInfoViewModel crivm,
            CelebrationRequestDetailsViewModel crdvm,
            CelebrationRequestPreviewViewModel crpvm,
            ICelebrationService celebrationService,
            DatabaseContext context)
        {
            Crivm = crivm;
            Crdvm = crdvm;
            Crpvm = crpvm;

            _celebrationService = celebrationService;
            _context = context;
            
            RegisterEventBusHandlers();
        }

        public void RegisterEventBusHandlers()
        {
            EventBus.RegisterHandler("CelebrationRequestFormForAdd", ForAdd);
            EventBus.RegisterHandler("CelebrationRequestFromForUpdate", celebration => ForUpdate(celebration as Celebration));
            
            EventBus.RegisterHandler("BackToCelebrationRequestInfo", () => Switch(Crivm));
            EventBus.RegisterHandler("BackToCelebrationRequestDetails", () => Switch(Crdvm));
            EventBus.RegisterHandler("NextToCelebrationRequestDetails", () => Switch(Crdvm));
            EventBus.RegisterHandler("NextToLongViewCelebration", () => Switch(Crpvm));
            
            EventBus.RegisterHandler("FinishAddCelebrationRequest", AddCelebration);
            EventBus.RegisterHandler("FinishUpdateCelebrationRequest", UpdateCelebration);
        }

        public void ForAdd()
        {
            Switch(Crivm);
            Crivm.ForAdd();
            Crdvm.ForAdd();
            Crivm.Celebration.CelebrationDetails = Crdvm.CelebrationDetails;
            Crpvm.ForAdd(Crivm.Celebration);
        }
        
        public void ForUpdate(Celebration celebration)
        {
            Switch(Crivm);
            Crivm.ForUpdate(celebration);
            Crdvm.ForUpdate(celebration.CelebrationDetails);
            Crivm.Celebration.CelebrationDetails = Crdvm.CelebrationDetails;
            Crpvm.ForUpdate(Crivm.Celebration);
        }

        public void AddCelebration()
        {
            EventBus.FireEvent("BackToClientPage");
            Console.WriteLine(Crivm.Celebration);
            Crivm.Celebration.ClientId = GlobalStore.ReadObject<BaseUser>("loggedUser").Id;
            _celebrationService.Create(Crivm.Celebration);
            EventBus.FireEvent("CelebrationAddSuccess");
        }
        
        public void UpdateCelebration()
        {
            EventBus.FireEvent("BackToClientPage");
            Crivm.Celebration.ClientId = GlobalStore.ReadObject<BaseUser>("loggedUser").Id;
            _celebrationService.Update(Crivm.Celebration);
            EventBus.FireEvent("CelebrationUpdateSuccess");
        }
    }
}