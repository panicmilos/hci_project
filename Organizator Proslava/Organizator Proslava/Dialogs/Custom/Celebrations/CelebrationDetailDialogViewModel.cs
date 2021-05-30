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

        public CelebrationDetailDialogViewModel(CelebrationDetail detail) : base("Pregled detalja", 660, 460)
        {
            CelebrationDetail = detail;

            Close = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, DialogResults.Undefined));
        }
    }
}