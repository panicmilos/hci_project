using Organizator_Proslava.Dialogs.Alert;
using Organizator_Proslava.Dialogs.Map;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.Cellebrations;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.UserCommands;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.Utils;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class CreateOrganizerViewModel : BaseObservableEntity, IDataErrorInfo
    {
        public Organizer Organizer { get; set; }

        public Address Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalId { get; set; }
        public string JMBG { get; set; }
        public string PhoneNumber { get; set; }
        public string _wholeAddress;
        public string WholeAddress { get => _wholeAddress; set => OnPropertyChanged(ref _wholeAddress, value); }
        public string UserName { get; set; }
        public string MailAddress { get; set; }
        public string CelebrationType { get; set; }

        private string _password;
        public string Password { get { return _password; } set { OnPropertyChanged(ref _password, value); OnPropertyChanged("RepeatedPassword"); } }

        private string _repeatedPassword;
        public string RepeatedPassword { get { return _repeatedPassword; } set { OnPropertyChanged(ref _repeatedPassword, value); OnPropertyChanged("Password"); } }

        // Validation
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Password" || columnName == "RepeatedPassword")
                    return Err(ValidationDictionary.Validate(columnName, Password, RepeatedPassword));

                if (columnName == "CelebrationType")
                    return Err(ValidationDictionary.Validate("NO" + columnName, CelebrationType, null));

                var valueOfProperty = GetType().GetProperty(columnName)?.GetValue(this);
                return Err(ValidationDictionary.Validate(columnName, valueOfProperty, null));
            }
        }

        public ICommand Create { get; set; }
        public ICommand Back { get; set; }
        public ICommand Map { get; set; }
        public ObservableCollection<string> CelebrationTypes { get; set; }

        public string Error => throw new NotImplementedException();

        private readonly IOrganizerService _organizerService;
        private readonly IDialogService _dialogService;
        private readonly ICelebrationTypeService _celebrationTypeService;
        private int _calls = 0;

        public CreateOrganizerViewModel(IOrganizerService organizerService, IDialogService dialogService, ICelebrationTypeService celebrationTypeService)
        {
            Organizer = new Organizer
            {
                Address = new Address()
            };
            _organizerService = organizerService;
            _dialogService = dialogService;
            _celebrationTypeService = celebrationTypeService;
            CelebrationTypes = new ObservableCollection<string>(_celebrationTypeService.ReadNames());

            RegisterHandlerToEventBus();

            Back = new RelayCommand(() => EventBus.FireEvent("OrganizersTableView"));

            Map = new RelayCommand(() =>
            {
                var address = _dialogService.OpenDialog(new MapDialogViewModel("Odaberi lokaciju"));
                if (address != null)
                {
                    Address = address;
                    WholeAddress = address?.WholeAddress;
                }
            });
        }

        private void RegisterHandlerToEventBus()
        {
            EventBus.RegisterHandler("CreateOrganizer", ForAdd);
            EventBus.RegisterHandler("UpdateOrganizer", organizer => ForUpdate(organizer));
        }

        public void ForUpdate(object o)
        {
            Organizer = o as Organizer;
            foreach (var property in Organizer.GetType().GetProperties())
            {
                GetType().GetProperty(property.Name)?.SetValue(this, property.GetValue(Organizer));
            }
            WholeAddress = Organizer.Address?.WholeAddress;
            RepeatedPassword = Organizer.Password;
            CelebrationType = Organizer.CellebrationType.Name;
            _calls = 11;

            Create = new RelayCommand<Organizer>(organizer =>
            {
                var optionDialogResult = _dialogService.OpenDialog(new OptionDialogViewModel("Potvrda", "Da li ste sigurni da želite da izmenite organizatora?"));
                if (optionDialogResult == Dialogs.DialogResults.Yes)
                {
                    CollectOrganizerInfo();
                    organizer.CellebrationType = _celebrationTypeService.ReadByName(CelebrationType);
                    if (organizer.CellebrationType == null)
                    {
                        organizer.CellebrationType = new CellebrationType
                        {
                            Name = CelebrationType
                        };
                    }

                    var currentOrganizerCopy = _organizerService.Read(organizer.Id).Clone();
                    var newOrganizerCopy = organizer.Clone();
                    GlobalStore.ReadObject<IUserCommandManager>("userCommands").Add(new UpdateOrganizer(currentOrganizerCopy, newOrganizerCopy));

                    _organizerService.Update(organizer);
                    EventBus.FireEvent("OrganizersTableView");
                    EventBus.FireEvent("ReloadOrganizerTable");
                    _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje", "Uspešno ste izmenili nalog."));
                }
            });
        }

        public void ForAdd()
        {
            Organizer = new Organizer();
            foreach (var property in Organizer.GetType().GetProperties())
            {
                GetType().GetProperty(property.Name)?.SetValue(this, property.GetValue(Organizer));
            }
            WholeAddress = string.Empty;
            RepeatedPassword = string.Empty;
            CelebrationType = string.Empty;
            _calls = 0;

            Create = new RelayCommand<Organizer>(o =>
            {
                var optionDialogResult = _dialogService.OpenDialog(new OptionDialogViewModel("Potvrda", "Da li ste sigurni da želite da kreirate novog organizatora?"));
                if (optionDialogResult == Dialogs.DialogResults.Yes)
                {
                    CollectOrganizerInfo();
                    o.Role = Role.Organizer;
                    o.CellebrationType = _celebrationTypeService.ReadByName(CelebrationType);
                    if (o.CellebrationType == null)
                    {
                        o.CellebrationType = new CellebrationType
                        {
                            Name = CelebrationType
                        };
                    }
                    _organizerService.Create(o);
                    EventBus.FireEvent("OrganizersTableView");
                    EventBus.FireEvent("ReloadOrganizerTable");
                    _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje", "Uspešno ste napravili nalog."));

                    GlobalStore.ReadObject<IUserCommandManager>("userCommands").Add(new CreateOrganizer(o));
                }
            });
        }

        private void CollectOrganizerInfo()
        {
            foreach (var property in GetType().GetProperties())
            {
                Organizer.GetType().GetProperty(property.Name)?.SetValue(Organizer, property.GetValue(this));
            }
        }

        private string Err(string message)
        {
            return message == null ? null : (_calls++ < 11 ? "*" : message);   // there are 7 fields
        }
    }
}