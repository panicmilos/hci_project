using Organizator_Proslava.Utility;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.DemoForm
{
    public class DemoViewModel : ObservableEntity
    {
        private IDictionary<string, string> _videos;

        public ObservableCollection<string> Functionalities { get; set; }

        private string _selectedVideo;
        public string SelectedVideo { get => _selectedVideo; set => OnPropertyChanged(ref _selectedVideo, value); }

        public ICommand PlayVideo { get; set; }

        public DemoViewModel()
        {
            _videos = new Dictionary<string, string>()
            {
                { "Funkcionalnost 1", "Ruta 1" },
                { "Funkcionalnost 2", "Ruta 2" },
                { "Funkcionalnost 3", "Ruta 3" },
                { "Funkcionalnost 4", "Ruta 4" },
                { "Funkcionalnost 5", "Ruta 5" },
                { "Funkcionalnost 6", "Ruta 6" },
                { "Funkcionalnost 7", "Ruta 7" },
                { "Funkcionalnost 8", "Ruta 8" },
                { "Funkcionalnost 9", "Ruta 9" },
            };
            SelectedVideo = "Funkcionalnost 7";

            Functionalities = new ObservableCollection<string>(_videos.Keys);

            PlayVideo = new RelayCommand<string>(functionalityName => SelectedVideo = functionalityName);
        }
    }
}