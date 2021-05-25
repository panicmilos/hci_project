using Organizator_Proslava.Model.Cellebrations;
using System;
using System.Collections.Generic;

namespace Organizator_Proslava.Services.Contracts
{
    public interface ICelebrationTypeService : ICrudService<CellebrationType>
    {
        IEnumerable<string> ReadNames();
        CellebrationType ReadByName(string name);
    }
}