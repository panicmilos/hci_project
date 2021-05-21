using Microsoft.Win32;
using Organizator_Proslava.Utility;
using System.Collections.ObjectModel;
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

        public CollaboratorImagesViewModel()
        {
            AddImage = new RelayCommand(AddImageHandler);
            ChangeMainImage = new RelayCommand<string>(file => MainImage = file);
            DeleteImage = new RelayCommand<string>(DeleteImageHandler);

            Back = new RelayCommand(() => EventBus.FireEvent("BackToCollaboratorServices"));
            Next = new RelayCommand(() => EventBus.FireEvent("NextToCollaboratorHalls"));
        }

        private void AddImageHandler()
        {
            var dialog = new OpenFileDialog() { Multiselect = false };
            dialog.Filter = @"Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
            bool? response = dialog.ShowDialog();

            if (response == true)
            {
                string file = dialog.FileName;

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
    }
}