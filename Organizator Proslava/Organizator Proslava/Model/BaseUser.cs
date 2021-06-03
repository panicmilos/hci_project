using System.ComponentModel.DataAnnotations;

namespace Organizator_Proslava.Model
{
    public class BaseUser : BaseObservableEntity
    {
        private string _firstName;
        public string FirstName { get => _firstName; set => OnPropertyChanged(ref _firstName, value); }

        private string _lastName;
        public string LastName { get => _lastName; set => OnPropertyChanged(ref _lastName, value); }

        private string _userName;
        
        public string FullName => $"{_firstName} {_lastName}";

        //[Required(ErrorMessage = "USERNAME is required")]
        //[StringLength(15, MinimumLength = 5, ErrorMessage = "UN min 5")]
        public string UserName
        {
            get => _userName;
            set
            {
                //ValidateProperty(value, "UserName");
                OnPropertyChanged(ref _userName, value);
            }
        }

        private string _password;
        public string Password { get => _password; set => OnPropertyChanged(ref _password, value); }

        private string _mailAddress;
        public string MailAddress { get => _mailAddress; set => OnPropertyChanged(ref _mailAddress, value); }

        private Role _role;
        public Role Role { get => _role; set => OnPropertyChanged(ref _role, value); }
    }
}