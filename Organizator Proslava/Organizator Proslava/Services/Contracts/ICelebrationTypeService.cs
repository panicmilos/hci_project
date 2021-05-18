using Organizator_Proslava.Model.Cellebrations;
using System.Collections.Generic;

namespace Organizator_Proslava.Services.Contracts
{
    public interface ICelebrationTypeService : ICrudService<CellebrationType>
    {
        IEnumerable<string> ReadNames();
    }
}