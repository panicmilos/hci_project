using Organizator_Proslava.Model;
using System.Collections.Generic;

namespace Organizator_Proslava.Services.Contracts
{
    public interface ICelebrationService : ICrudService<Celebration>
    {
        IEnumerable<Celebration> ReadNotTaken();
    }
}