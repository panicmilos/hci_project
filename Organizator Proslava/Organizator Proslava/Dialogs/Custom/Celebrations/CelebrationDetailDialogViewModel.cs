using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Celebrations
{
    public class CelebrationDetailDialogViewModel : DialogViewModelBase<DialogResults>
    {
        public CelebrationDetail CelebrationDetail { get; set; }
        public ICommand Close { get; set; }
        public ICommand Back { get; set; }

        public bool IsBack { get; set; }
        public bool IsClose { get; set; }

        public CelebrationDetailDialogViewModel() : base("Pregled detalja", 660, 460)
        {
            Close = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, DialogResults.Undefined));

            Back = new RelayCommand(() => EventBus.FireEvent("BackToDetailsTable"));
        }
    }
}