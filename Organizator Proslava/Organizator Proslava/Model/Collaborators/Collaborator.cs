namespace Organizator_Proslava.Model.Collaborators
{
    public class Collaborator : BaseUser
    {
        private string _phoneNumber;
        public string PhoneNumber { get => _phoneNumber; set => OnPropertyChanged(ref _phoneNumber, value); }

        private Address _address;
        public Address Address { get => _address; set => OnPropertyChanged(ref _address, value); }

        private CollaboratorServiceBook _collaboratorServiceBook;
        public CollaboratorServiceBook CollaboratorServiceBook { get => _collaboratorServiceBook; set => OnPropertyChanged(ref _collaboratorServiceBook, value); }
    }
}