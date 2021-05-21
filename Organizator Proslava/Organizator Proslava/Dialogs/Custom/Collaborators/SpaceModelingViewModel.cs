using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    public class SpaceModelingViewModel : DialogViewModelBase<CelebrationHall>
    {
        private SpaceViewModel _viewModel;
        public SpaceViewModel ViewModel { get => _viewModel; set => OnPropertyChanged(ref _viewModel, value); }

        public ICommand Save { get; set; }
        public ICommand Back { get; set; }

        public SpaceModelingViewModel(SpaceViewModel spv) :
            base("Modelovanje prostora", 800, 550)
        {
            ViewModel = spv;

            Save = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, ViewModel.Hall));

            Back = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, null));
        }
    }
}