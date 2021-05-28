using Organizator_Proslava.Dialogs.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizator_Proslava.ViewModel.OrganizatorHome
{
    public class AcceptCelebrationRequestTableViewModel
    {
        private readonly IDialogService _dialogService;

        public AcceptCelebrationRequestTableViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public void Reload()
        {
        }
    }
}