using Organizator_Proslava.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organizator_Proslava.ViewModel
{
    public class MainViewModel
    {
        public String SomeText { get; set; }

        private readonly IDummyService _dummyService;

        public MainViewModel(IDummyService dummyService)
        {
            _dummyService = dummyService;
            SomeText = _dummyService.What();
        }
    }
}