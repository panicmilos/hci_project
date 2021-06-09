using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Organizator_Proslava.Services.Contracts;

namespace Organizator_Proslava.ViewModel.CelebrationRequestForm
{
    public class ChooseOrganizerViewModel : DialogViewModelBase<Organizer>
    {
        public OrganizerWithNumOfDoneCelebrations SelectedOrganizer { get; set; }

        public ICommand Choose { get; set; }
        public ICommand Cancel { get; set; }

        public ObservableCollection<OrganizerWithNumOfDoneCelebrations> Organizers { get; set; }

        public ChooseOrganizerViewModel(string celebrationType, IOrganizerService organizerService, ICelebrationService celebrationService) : base("Odabir organizatora", 560, 360)
        {
            Organizers =
                new ObservableCollection<OrganizerWithNumOfDoneCelebrations>(organizerService.ReadSpecifiedFor(celebrationType).ToList()
                    .Select(organizer =>
                    {
                        var organizerWithNumOfDoneCelebrations = new OrganizerWithNumOfDoneCelebrations();
                        organizerWithNumOfDoneCelebrations.CopyFields(organizer);
                        organizerWithNumOfDoneCelebrations.NumOfDoneCelebrations =
                            celebrationService.GetNumOfDoneCelebrationsForOrganizer(organizer.Id);
                        return organizerWithNumOfDoneCelebrations;
                    }));

            Choose = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, SelectedOrganizer));
            Cancel = new RelayCommand<IDialogWindow>(window => CloseDialogWithResult(window, null));
        }
    }

    public class OrganizerWithNumOfDoneCelebrations : Organizer
    {
        private int _numOfDoneCelebrations;
        public int NumOfDoneCelebrations { get => _numOfDoneCelebrations; set => OnPropertyChanged(ref _numOfDoneCelebrations, value); }

        public void CopyFields(Organizer organizer)
        {
            Id = organizer.Id;
            IsActive = organizer.IsActive;
            CreatedAt = organizer.CreatedAt;
            MailAddress = organizer.MailAddress;
            FirstName = organizer.FirstName;
            LastName = organizer.LastName;
            UserName = organizer.UserName;
            Password = organizer.Password;
            PhoneNumber = organizer.PhoneNumber;
            Role = organizer.Role;
            PersonalId = organizer.PersonalId;
            JMBG = organizer.JMBG;
            Address = organizer.Address?.Clone();
            CellebrationType = organizer.CellebrationType?.Clone();
        }
    }
}