namespace Organizator_Proslava.Model
{
    public class Client : BaseUser
    {
        private string _phoneNumber;
        public string PhoneNumber { get => _phoneNumber; set => OnPropertyChanged(ref _phoneNumber, value); }
    }
}