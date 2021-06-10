using Organizator_Proslava.Data;
using Organizator_Proslava.Model;
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
            return _context.BaseUsers.Any(u => u.UserName == username);
        }

        public BaseUser Authenticate(string username, string password)
        {
            return _entities.FirstOrDefault(u => u.UserName == username && u.Password == password);
        }

        public bool IsEmailUsed(string email)
        {
            return _context.BaseUsers.Any(u => u.MailAddress == email);
        }
    }
}