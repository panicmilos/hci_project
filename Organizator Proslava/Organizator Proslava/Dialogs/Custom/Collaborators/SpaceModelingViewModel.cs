using Organizator_Proslava.Dialogs.Alert;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel;
using System.Linq;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    public class SpaceModelingViewModel : DialogViewModelBase<CelebrationHall>
    {
        private SpaceViewModel _viewModel;
        public SpaceViewModel ViewModel { get => _viewModel; set => OnPropertyChanged(ref _viewModel, value); }

        public ICommand Save { get; set; }
        public ICommand Back { get; set; }

        private readonly IDialogService _dialogService;

        public SpaceModelingViewModel(SpaceViewModel spv) :
            base("Modelovanje prostora", 800, 550)
        {
            _dialogService = new DialogService();

            ViewModel = spv;

            Save = new RelayCommand<IDialogWindow>(w =>
            {
                if (!_viewModel.Hall.PlaceableEntities.Any())
                {
                    _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje", "Morate postaviti bar jedan sto."));
                    return;
                }
                CloseDialogWithResult(w, ViewModel.Hall);
            });

            Back = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, null));
        }
    }
}