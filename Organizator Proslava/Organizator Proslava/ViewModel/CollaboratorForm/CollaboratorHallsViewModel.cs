using Organizator_Proslava.Data;
using Organizator_Proslava.Dialogs;
using Organizator_Proslava.Dialogs.Custom.Collaborators;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CollaboratorForm
{
    public class CollaboratorHallsViewModel : ObservableEntity
    {
        private List<CelebrationHall> _cellbrationHalls;
        public List<CelebrationHall> CollaboratorServiceBook { get => _cellbrationHalls; set => OnPropertyChanged(ref _cellbrationHalls, value); }

        public ObservableCollection<CelebrationHall> Halls { get; set; }

        private IDialogService _dialogService;

        public ICommand Back { get; set; }
        public ICommand Next { get; set; }

        public ICommand Add { get; set; }
        public ICommand Edit { get; set; }
        public ICommand Remove { get; set; }

        public CollaboratorHallsViewModel(IDialogService dialogService, DatabaseContext context)
        {
            _dialogService = dialogService;

            CollaboratorServiceBook = new List<CelebrationHall>()
            {
                new CelebrationHall
                {
                    Name = "Sala 1",
                    NumberOfGuests = 40
                },
                new CelebrationHall
                {
                    Name = "Sala 2",
                    NumberOfGuests = 20
                }
            };

            Halls = new ObservableCollection<CelebrationHall>(_cellbrationHalls);

            Add = new RelayCommand(() =>
            {
                var hall = _dialogService.OpenDialog(new SpaceModelingViewModel(new SpaceViewModel()));
                if (hall != null)
                {
                    Halls.Add(hall);
                    CollaboratorServiceBook.Add(hall);
                }
            });

            Edit = new RelayCommand<CelebrationHall>(hall =>
            {
                var hallCopy = hall.Copy();
                var editedHall = dialogService.OpenDialog(new SpaceModelingViewModel(new SpaceViewModel(hallCopy)));

                if (editedHall != null)
                {
                    hall.Name = editedHall.Name;
                    hall.NumberOfGuests = editedHall.NumberOfGuests;
                    hall.PlaceableEntities = editedHall.PlaceableEntities;
                }
            });

            Remove = new RelayCommand<CelebrationHall>(hall =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Pitanje", "Da li ste sigurni da želite da obrišete ovu salu?")) == DialogResults.Yes)
                {
                    Halls.Remove(hall);
                    CollaboratorServiceBook.Remove(hall);
                }
            });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToCollaboratorImages"));
        }
    }
}