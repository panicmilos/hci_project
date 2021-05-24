using Organizator_Proslava.Model.Cellebrations;
using System;

namespace Organizator_Proslava.Model
{
    public class Organizer : BaseUser
    {
        private string _personalId;
        public string PersonalId { get => _personalId; set => OnPropertyChanged(ref _personalId, value); }

        private string _jmbg;
        public string JMBG { get => _jmbg; set => OnPropertyChanged(ref _jmbg, value); }

        private string _phoneNumber;
        public string PhoneNumber { get => _phoneNumber; set => OnPropertyChanged(ref _phoneNumber, value); }

        private Address _address;
        public Address Address { get => _address; set => OnPropertyChanged(ref _address, value); }

        private CellebrationType _cellebrationType;
        public CellebrationType CellebrationType { get => _cellebrationType; set => OnPropertyChanged(ref _cellebrationType, value); }
    }
}