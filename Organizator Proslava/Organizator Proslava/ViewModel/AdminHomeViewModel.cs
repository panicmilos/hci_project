using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.Celebrations;
using Organizator_Proslava.ViewModel.UsersView;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class AdminHomeViewModel
    {
        public ICommand Back { get; set; }
        public ICommand Organizers { get; set; }
        public ICommand Clients { get; set; }
        public ICommand Collaborators { get; set; }
        public ICommand Celebrations { get; set; }

        private readonly OrganziersTableViewModel _otvm;
        private readonly UsersTableViewModel _utvm;
        private readonly CelebrationsTableViewModel _celbtvm;
        private readonly CollaboratorsTableViewModel _colbtvm;

        public AdminHomeViewModel(OrganziersTableViewModel otvm, UsersTableViewModel utvm, CelebrationsTableViewModel celbtvm, CollaboratorsTableViewModel colbtvm)
        {
            _otvm = otvm;
            _utvm = utvm;
            _celbtvm = celbtvm;
            _colbtvm = colbtvm;

            Back = new RelayCommand(() => EventBus.FireEvent("BackToLogin"));
            Organizers = new RelayCommand(() => EventBus.FireEvent("SwitchMainViewModel", _otvm));
            Clients = new RelayCommand(() => EventBus.FireEvent("SwitchMainViewModel", _utvm));
            Collaborators = new RelayCommand(() => { _colbtvm.ForAdministrator(); EventBus.FireEvent("SwitchMainViewModel", _colbtvm); });
            Celebrations = new RelayCommand(() => EventBus.FireEvent("SwitchMainViewModel", _celbtvm));

            EventBus.RegisterHandler("OrganizersTableView", () => Organizers.Execute(null));
            EventBus.RegisterHandler("ClientsTableView", () => Clients.Execute(null));
            EventBus.RegisterHandler("CollaboratorsTableView", () => Collaborators.Execute(null));
            EventBus.RegisterHandler("CelebrationsTableView", () => Celebrations.Execute(null));

            EventBus.RegisterHandler("BackToCollaboratorsForAdmin", () => Collaborators.Execute(null));
        }
    }
}