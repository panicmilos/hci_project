using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Collaborators
{
    public class DisplayImagesViewModel : DialogViewModelBase<DialogResults>
    {
        public ICommand AddImage { get; set; }
        public ICommand ChangeMainImage { get; set; }
        public ICommand DeleteImage { get; set; }

        public ICommand Back { get; set; }
        public ICommand Next { get; set; }

        public bool ShowForAdd { get; set; }

        private const string ImagePlaceholderFileName = @"pack://siteoforigin:,,,,/Resources/placeholder.png";

        public ObservableCollection<string> Images { get; set; } = new ObservableCollection<string>() {
            ImagePlaceholderFileName
        };

        private string _mainImage = ImagePlaceholderFileName;
        public string MainImage { get => _mainImage; set => OnPropertyChanged(ref _mainImage, value); }

        //private readonly ICollaboratorService _collaboratorService;
        private readonly IDialogService _dialogService;

        public DisplayImagesViewModel(Collaborator collaborator):
            base("Pregled slika", 650, 700)
        {
            _dialogService = new DialogService();
            ShowForAdd = false;
            Images = new ObservableCollection<string>(collaborator.Images);
            ChangeMainImage = new RelayCommand<string>(file => MainImage = file);
            MainImage = Images.Any() ? collaborator.Images[0] : ImagePlaceholderFileName;
            Back = new RelayCommand<IDialogWindow>(window =>
            {
                CloseDialogWithResult(window, DialogResults.Undefined);

                if(collaborator is IndividualCollaborator)
                    _dialogService.OpenDialog(new IndividualCollaboratorDetailViewModel(collaborator as IndividualCollaborator));
                else
                    _dialogService.OpenDialog(new LegalCollaboratorDetailViewModel(collaborator as LegalCollaborator));
            });
        }
    }
}
