using Organizator_Proslava.Data;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;
using Organizator_Proslava.Services.Contracts;
using System.Linq;

namespace Organizator_Proslava.Services.Implementations
{
    public class UserService<T> : CrudService<T>, IUserService<T> where T : BaseUser
    {
        public UserService(DatabaseContext context) :
            base(context)
        {
        }

        public bool AlreadyInUse(string username)
        {
            return base.Read().Any(u => u.UserName == username);
        }

        public BaseUser Authenticate(string username, string password)
        {
            var user = base.Read().FirstOrDefault(u => u.UserName == username && u.Password == password);
            if (user != null)
                GlobalStore.AddObject("loggedUser", user);
            return user;
        }
    }
}