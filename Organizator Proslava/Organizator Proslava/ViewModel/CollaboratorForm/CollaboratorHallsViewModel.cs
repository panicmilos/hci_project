using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public CollaboratorHallsViewModel(IDialogService dialogService)
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

            Add = new RelayCommand(() => { });

            Edit = new RelayCommand<CelebrationHall>(service => { });

            Remove = new RelayCommand<CelebrationHall>(service => { });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToCollaboratorServices"));
        }
    }
}