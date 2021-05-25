using Organizator_Proslava.Data;
using Organizator_Proslava.Model.Cellebrations;
using Organizator_Proslava.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Organizator_Proslava.Services.Implementations
{
    public class CelebrationTypeService : CrudService<CellebrationType>, ICelebrationTypeService
    {
        public CelebrationTypeService(DatabaseContext context) :
            base(context)
        {
        }

        public CellebrationType ReadByName(string name)
        {
            var cellebrationType = Read().FirstOrDefault(type => type.Name == name);
            return cellebrationType;
        }

        public IEnumerable<string> ReadNames()
        {
            return Read().Select(celebrationType => celebrationType.Name);
        }
    }
}