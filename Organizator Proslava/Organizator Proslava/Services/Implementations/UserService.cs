using Organizator_Proslava.Data;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;

namespace Organizator_Proslava.Services.Implementations
{
    public class UserService<T> : CrudService<T>, IUserService<T> where T : BaseUser
    {
        public UserService(DatabaseContext context) :
            base(context)
        {
        }
    }
}