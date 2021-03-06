using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Utility;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    public class DisplayHallsDialogViewModel : DialogViewModelBase<DialogResults>
    {
        public ObservableCollection<CelebrationHall> Halls { get; set; }

        private readonly IDialogService _dialogService;

        public ICommand Back { get; set; }
        public ICommand Details { get; set; }

        public Collaborator Collaborator { get; set; }

        public DisplayHallsDialogViewModel() :
            base("Pregled saradnikovih sala", 680, 420)
        {
            _dialogService = new DialogService();

            Back = new RelayCommand<IDialogWindow>(window => EventBus.FireEvent("BackToInformations"));

            Details = new RelayCommand<CelebrationHall>(hall => EventBus.FireEvent("DisplayOneHall", hall));
        }
    }
}