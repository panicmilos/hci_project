using Organizator_Proslava.Utility;

namespace Organizator_Proslava.Model.DTO
{
    public class LoginDTO : ObservableEntity
    {
        private string _username;
        public string UserName { get => _username; set => OnPropertyChanged(ref _username, value); }

        private string _password;
        public string Password { get => _password; set => OnPropertyChanged(ref _password, value); }
    }
}