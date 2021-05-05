using Organizator_Proslava.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizator_Proslava.Theme
{
    /// <summary>
    /// This class exists only so that this folder will be pushed on git.
    /// Remove this class when this folder have at least one real class.
    /// </summary>
    public class DummyService : IDummyService
    {
        public string What()
        {
            return "I am dummy service";
        }
    }
}