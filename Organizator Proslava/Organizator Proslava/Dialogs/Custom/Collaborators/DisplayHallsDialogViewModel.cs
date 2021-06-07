using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    public class DisplayHallsDialogViewModel : DialogViewModelBase<DialogResults>
    {
        public ObservableCollection<CelebrationHall> Halls { get; set; }

        private readonly IDialogService _dialogService;

        public ICommand Back { get; set; }
        public ICommand Details { get; set; }

        public DisplayHallsDialogViewModel(Collaborator collaborator) :
            base("Pregled sala saradnika", 680, 420)
        {
            _dialogService = new DialogService();

            Halls = new ObservableCollection<CelebrationHall>(collaborator.CelebrationHalls);

            Back = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, DialogResults.Undefined));

            Details = new RelayCommand<CelebrationHall>(hall => _dialogService.OpenDialog(new SpacePreviewDialogViewModel(hall, _dialogService)), (hall) => hall != null);
        }
    }
}