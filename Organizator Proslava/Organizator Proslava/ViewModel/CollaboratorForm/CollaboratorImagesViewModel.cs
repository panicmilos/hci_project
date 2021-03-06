using Microsoft.Win32;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.UserCommands;
using Organizator_Proslava.Utility;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CollaboratorForm
{
    public class CollaboratorImagesViewModel : ObservableEntity
    {
        private const string ImagePlaceholderFileName = @"pack://siteoforigin:,,,/Resources/placeholder.png";

        public ObservableCollection<string> Images { get; set; } = new ObservableCollection<string>() {
            ImagePlaceholderFileName
        };

        private string _mainImage = ImagePlaceholderFileName;
        public string MainImage { get => _mainImage; set => OnPropertyChanged(ref _mainImage, value); }

        public ICommand AddImage { get; set; }
        public ICommand ChangeMainImage { get; set; }
        public ICommand DeleteImage { get; set; }

        public ICommand Back { get; set; }
        public ICommand Next { get; set; }

        public bool ShowForAdd { get; set; }

        public CollaboratorImagesViewModel()
        {
            ShowForAdd = true;
            AddImage = new RelayCommand(ChooseAndAddImage);
            ChangeMainImage = new RelayCommand<string>(file => MainImage = file);
            DeleteImage = new RelayCommand<string>(f => { DeleteImageHandler(f); GlobalStore.ReadObject<IUserCommandManager>("userCommands").Add(new DeleteImage(f, AddImageHandler, DeleteImageHandler)); });

            Back = new RelayCommand(() => EventBus.FireEvent("BackToCollaboratorServices"));
            Next = new RelayCommand(() => EventBus.FireEvent("NextToCollaboratorHalls"));
        }

        private void ChooseAndAddImage()
        {
            var dialog = new OpenFileDialog() { Multiselect = false };
            dialog.Filter = @"Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
            bool? response = dialog.ShowDialog();

            if (response == true)
            {
                AddImageHandler(dialog.FileName);
                GlobalStore.ReadObject<IUserCommandManager>("userCommands").Add(new AddImage(dialog.FileName, AddImageHandler, DeleteImageHandler));
            }
        }

        private void AddImageHandler(string file)

        {
            if (Images.Count == 1 && Images[0] == ImagePlaceholderFileName)
            {
                Images[0] = file;
                MainImage = file;
            }
            else
            {
                Images.Add(file);
            }
        }

        private void DeleteImageHandler(string file)
        {
            if (Images.Count == 1)
            {
                Images[0] = ImagePlaceholderFileName;
                MainImage = ImagePlaceholderFileName;
            }
            else
            {
                Images.Remove(file);
                if (MainImage == file)
                {
                    MainImage = Images[0];
                }
            }
        }

        public void ForAdd()
        {
            Images = new ObservableCollection<string>() { ImagePlaceholderFileName };
            MainImage = ImagePlaceholderFileName;
        }

        public void ForUpdate(Collaborator collaborator)
        {
            Images = new ObservableCollection<string>(collaborator.Images);
            MainImage = collaborator.Images.Any() ? collaborator.Images[0] : ImagePlaceholderFileName;
        }
    }
}