using Organizator_Proslava.Dialogs.Option;
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

        private readonly IDialogService _dialogService;

        public SpacePreviewDialogViewModel(SpacePreviewViewModel spvm, IDialogService dialogService) :
            base("Pregled prostora", 650, 550)
        {
            ViewModel = spvm;

            _dialogService = dialogService;

            Save = new RelayCommand<IDialogWindow>(w =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Pitanje", "Da li ste sigurni da želite da sačuvate ovaj raspored sale?")) == DialogResults.Yes)
                {
                    CloseDialogWithResult(w, ViewModel.Hall);
                }
            });

            Back = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, null));
        }
    }
}