using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Demo
{
    public class DemoDialogViewModel : DialogViewModelBase<DialogResults>

    {
        private readonly IDictionary<string, string> _videos;

        public ObservableCollection<string> Functionalities { get; set; }

        private string _selectedVideo;
        public string SelectedVideo { get => _selectedVideo; set => OnPropertyChanged(ref _selectedVideo, value); }

        public ICommand PlayVideo { get; set; }

        public DemoDialogViewModel(Type dataContextType) :
            base("Demo", 860, 660)
        {
            _videos = new Dictionary<string, string>()
            {
                { "Funkcionalnost 1", "somecatvideo.mp4" },
                { "Funkcionalnost 2", "somekidheadshot.mp4" },
                { "Funkcionalnost 3", "Ruta 3" },
                { "Funkcionalnost 4", "Ruta 4" },
                { "Funkcionalnost 5", "Ruta 5" },
                { "Funkcionalnost 6", "Ruta 6" },
                { "Funkcionalnost 7", "Ruta 7" },
                { "Funkcionalnost 8", "Ruta 8" },
                { "Funkcionalnost 9", "Ruta 9" },
            };

            var _typeToStringDictionary = new Dictionary<Type, string>
            {
                { typeof(RegisterViewModel), "Funkcionalnost 2" },
            };

            var startingFunctionalityName = _typeToStringDictionary.ContainsKey(dataContextType) ? _typeToStringDictionary[dataContextType] : "Funkcionalnost 1";
            SelectedVideo = GetPathBasedOnFunctionalityName(startingFunctionalityName);

            Functionalities = new ObservableCollection<string>(_videos.Keys);

            PlayVideo = new RelayCommand<string>(functionalityName =>
            {
                SelectedVideo = GetPathBasedOnFunctionalityName(functionalityName);
                EventBus.FireEvent("demoFunctinalityChanged");
            });
        }

        private string GetPathBasedOnFunctionalityName(string functionalityName)
        {
            return "pack://siteoforigin:,,,/Resources/" + _videos[functionalityName]; ;
        }
    }
}