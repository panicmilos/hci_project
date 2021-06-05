using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Clients
{
    public class ClientDetailViewModel : DialogViewModelBase<DialogResults>
    {
        public Client Client { get; set; }
        public ICommand Back { get; set; }

        public ClientDetailViewModel(Client client):
            base("Pregled korisnika", 490, 420)
        {
            Client = client;
            Back = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, DialogResults.Undefined));
        }
    }
}
