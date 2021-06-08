using Organizator_Proslava.Model.Cellebrations;
using System;

namespace Organizator_Proslava.Model
{
    public class Organizer : BaseUser, ICloneable<Organizer>
    {
        private string _personalId;
        public string PersonalId { get => _personalId; set => OnPropertyChanged(ref _personalId, value); }

        private string _jmbg;
        public string JMBG { get => _jmbg; set => OnPropertyChanged(ref _jmbg, value); }

        private string _phoneNumber;
        public string PhoneNumber { get => _phoneNumber; set => OnPropertyChanged(ref _phoneNumber, value); }

        private Guid _addressId;
        public Guid AddressId { get => _addressId; set => OnPropertyChanged(ref _addressId, value); }

        private Address _address;
        public virtual Address Address { get => _address; set => OnPropertyChanged(ref _address, value); }

        private Guid _cellebrationTypeId;
        public Guid CellebrationTypeId { get => _cellebrationTypeId; set => OnPropertyChanged(ref _cellebrationTypeId, value); }

        private CellebrationType _cellebrationType;
        public virtual CellebrationType CellebrationType { get => _cellebrationType; set => OnPropertyChanged(ref _cellebrationType, value); }

        public Organizer Clone()
        {
            return new Organizer
            {
                Id = Id,
                IsActive = IsActive,
                CreatedAt = CreatedAt,
                MailAddress = MailAddress,
                FirstName = FirstName,
                LastName = LastName,
                UserName = UserName,
                Password = Password,
                PhoneNumber = PhoneNumber,
                Role = Role,
                PersonalId = PersonalId,
                JMBG = JMBG,
                Address = Address?.Clone(),
                CellebrationType = CellebrationType?.Clone()
            };
        }
    }
}