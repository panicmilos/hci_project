namespace Organizator_Proslava.Model
{
    public class Collaborator : BaseUser
    {
        private string _phoneNumber;
        public string PhoneNumber { get => _phoneNumber; set => OnPropertyChanged(ref _phoneNumber, value); }

        private Address _address;
        public Address Address { get => _address; set => OnPropertyChanged(ref _address, value); }
    }
}