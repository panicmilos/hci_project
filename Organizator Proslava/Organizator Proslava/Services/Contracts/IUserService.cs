using Organizator_Proslava.Model;

namespace Organizator_Proslava.Services.Contracts
{
    public interface IUserService<T> : ICrudService<T> where T : BaseUser
    {
        BaseUser Authenticate(string username, string password);
    }
}