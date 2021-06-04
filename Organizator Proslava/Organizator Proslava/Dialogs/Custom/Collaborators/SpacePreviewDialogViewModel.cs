using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationHalls;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    public class SpacePreviewDialogViewModel : DialogViewModelBase<CelebrationHall>
    {
        private SpacePreviewViewModel _viewModel;
        public SpacePreviewViewModel ViewModel { get => _viewModel; set => OnPropertyChanged(ref _viewModel, value); }

        public bool ShouldShowSave { get => ViewModel.Mode == SpacePreviewMode.Edit; }

        public ICommand Save { get; set; }
        public ICommand Back { get; set; }

        public SpacePreviewDialogViewModel(SpacePreviewViewModel spvm) :
            base("Pregled prostora", 650, 550)
        {
            ViewModel = spvm;

            Save = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, ViewModel.Hall));

            Back = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, null));
        }
    }
}