using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Celebrations
{
    public class CelebrationsDetailsTableDialogViewModel : DialogViewModelBase<DialogResults>
    {
        public ObservableCollection<CelebrationDetail> CelebrationDetails { get; set; }

        public ICommand Preview { get; set; }
        public ICommand Proposals { get; set; }
        public ICommand Back { get; set; }

        private readonly IDialogService _dialogService;

        public CelebrationsDetailsTableDialogViewModel(IDialogService dialogService, Celebration celebration)
        {
            _dialogService = dialogService;

            CelebrationDetails = new ObservableCollection<CelebrationDetail>(celebration.CelebrationDetails);

            Back = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, DialogResults.Undefined));

            Preview = new RelayCommand<CelebrationDetail>(celebrationDetail =>
            {
                EventBus.FireEvent("CelebrationDetailPreview", celebrationDetail);
            });
        }
    }
}
