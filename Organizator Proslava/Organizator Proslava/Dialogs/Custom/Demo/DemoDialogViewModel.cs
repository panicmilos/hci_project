using Organizator_Proslava.Dialogs.Custom.Notifications;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel;
using Organizator_Proslava.ViewModel.CelebrationResponseForm;
using Organizator_Proslava.ViewModel.CollaboratorForm;
using Organizator_Proslava.ViewModel.OrganizatorHome;
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
                { "Funkcionalnosti organizatora", "full.wmv" },
                { "Pravljenje naloga", "registracija.wmv" },
                { "Prijava", "login.wmv" },
                { "Pregled saradnika", "pregled saradnika.wmv" },
                { "Dodavanje novog saradnika", "dodavanje saradnika.wmv" },
                { "Izmena saradnika", "izmena saradnika.wmv" },
                { "Brisanje saradnika", "brisanje saradnika.wmv" },
                { "Pregled proslava", "pregled proslava.wmv" },
                { "Prihvatanje zahteva", "prihvatanje.wmv" },
                { "Davanje ponude", "ponuda i komentar.wmv" },
                { "Otkazivanje proslave", "otkazivanje.wmv" },
                { "Obaveštenja", "norf.wmv" },
                { "Izlazak", "logout.wmv" },
            };
            var _typeToStringDictionary = new Dictionary<Type, string>
            {
                { typeof(RegisterViewModel), "Pravljenje naloga" },
                { typeof(CollaboratorsTableViewModel), "Pregled saradnika" },
                { typeof(CollaboratorFormViewModel), "Dodavanje novog saradnika" },
                { typeof(OrganizersPastCelebrationsTableViewModel), "Pregled proslava" },
                { typeof(AcceptCelebrationRequestTableViewModel), "Prihvatanje zahteva" },
                { typeof(NotificationsDialogViewModel), "Obaveštenja" },
                { typeof(CelebrationResponseFormViewModel), "Davanje ponude" }
            };

            var startingFunctionalityName = _typeToStringDictionary.ContainsKey(dataContextType) ? _typeToStringDictionary[dataContextType] : "Funkcionalnosti organizatora";
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