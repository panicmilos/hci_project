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

        public CellebrationType GetCelebrationType(string name)
        {
            var cellebrationType = Read().Where(type => type.Name == name).FirstOrDefault();
            return cellebrationType;
        }

        public IEnumerable<string> ReadNames()
        {
            return Read().Select(celebrationType => celebrationType.Name);
        }
    }
}