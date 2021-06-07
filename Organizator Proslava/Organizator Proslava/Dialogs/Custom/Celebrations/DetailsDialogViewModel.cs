using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizator_Proslava.Dialogs.Custom.Celebrations
{
    public class DetailsDialogViewModel : NavigableDialogViewModelBase<DialogResults>
    {
        public CelebrationsDetailsTableDialogViewModel CelebrationsDetailsTableDialogViewModel { get; set; }
        public CelebrationDetailDialogViewModel CelebrationDetailDialogViewModel { get; set; }

        public DetailsDialogViewModel(CelebrationsDetailsTableDialogViewModel celebrationsDetailsTableDialogViewModel):
            base("Pregled detalja proslave", 650, 490)
        {
            CelebrationsDetailsTableDialogViewModel = celebrationsDetailsTableDialogViewModel;

            Switch(CelebrationsDetailsTableDialogViewModel);

            RegisterHandlerToEventBus();
        }

        public void RegisterHandlerToEventBus()
        {
            EventBus.RegisterHandler("CelebrationDetailPreview", detail =>
            {
                CelebrationDetailDialogViewModel = new CelebrationDetailDialogViewModel(detail as CelebrationDetail, true, false);
                Switch(CelebrationDetailDialogViewModel);
            });
            EventBus.RegisterHandler("BackToDetailsTable", () => Switch(CelebrationsDetailsTableDialogViewModel));
        }
    }
}
