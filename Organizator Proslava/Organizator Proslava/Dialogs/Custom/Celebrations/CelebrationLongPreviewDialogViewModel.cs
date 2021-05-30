using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Celebrations
{
    public class CelebrationLongPreviewDialogViewModel : DialogViewModelBase<DialogResults>
    {
        public Celebration Celebration { get; set; }

        public ICommand Close { get; set; }

        public CelebrationLongPreviewDialogViewModel(Celebration celebration) :
            base("Pregled proslave", 660, 460)
        {
            Celebration = celebration;
            Close = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, DialogResults.Undefined));
        }
    }
}