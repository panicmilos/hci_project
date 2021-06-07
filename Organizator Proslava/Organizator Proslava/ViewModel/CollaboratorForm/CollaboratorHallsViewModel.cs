using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Custom.Collaborators;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CollaboratorForm
{
    public class CollaboratorHallsViewModel : ObservableEntity
    {
        private List<CelebrationHall> _cellbrationHalls;
        public List<CelebrationHall> CelebrationHalls { get => _cellbrationHalls; set => OnPropertyChanged(ref _cellbrationHalls, value); }

        public ObservableCollection<CelebrationHall> Halls { get; set; }

        private readonly IDialogService _dialogService;

        public ICommand Back { get; set; }
        public ICommand Save { get; set; }

        public ICommand Add { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Remove { get; set; }

        public CollaboratorHallsViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            CelebrationHalls = new List<CelebrationHall>();
            Halls = new ObservableCollection<CelebrationHall>(_cellbrationHalls);

            Add = new RelayCommand(() =>
            {
                var hall = _dialogService.OpenDialog(new SpaceModelingViewModel());
                if (hall != null)
                {
                    Halls.Add(hall);
                    CelebrationHalls.Add(hall);
                }
            });

            Edit = new RelayCommand<CelebrationHall>(hall =>
            {
                var hallCopy = hall.Clone();
                var editedHall = dialogService.OpenDialog(new SpaceModelingViewModel(hallCopy));

                if (editedHall != null)
                {
                    hall.Name = editedHall.Name;
                    hall.PlaceableEntities = editedHall.PlaceableEntities;
                }
            });

            Remove = new RelayCommand<CelebrationHall>(hall =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Pitanje", "Da li ste sigurni da želite da obrišete ovu salu?")) == DialogResults.Yes)
                {
                    Halls.Remove(hall);
                    CelebrationHalls.Remove(hall);
                }
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToCollaboratorImages"));
        }

        public void ForAdd()
        {
            CelebrationHalls = new List<CelebrationHall>();
            Halls = new ObservableCollection<CelebrationHall>();
            Save = new RelayCommand(() =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Pitanje", "Da li ste sigurni da želite da dodate ovog saradnika?")) == DialogResults.Yes)
                {
                    EventBus.FireEvent("AddCollaborator");
                }
            });
        }

        public void ForUpdate(Collaborator collaborator)
        {
            CelebrationHalls = collaborator.CelebrationHalls;
            Halls = new ObservableCollection<CelebrationHall>(collaborator.CelebrationHalls);
            Save = new RelayCommand(() =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Pitanje", "Da li ste sigurni da izmeniti ovog saradnika?")) == DialogResults.Yes)
                {
                    EventBus.FireEvent("UpdateCollaborator");
                }
            });
        }
    }
}