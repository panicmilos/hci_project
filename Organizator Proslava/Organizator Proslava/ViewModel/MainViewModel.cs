using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System;
using System.Diagnostics;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel
{
    public class MainViewModel
    {
        public String SomeText { get; set; }

        public DummyClass Dummy { get; set; } = new DummyClass();
        public ICommand _log { get; set; }

        private readonly IDummyService _dummyService;

        public MainViewModel(IDummyService dummyService)
        {
            _log = new RelayCommand<string>(s =>
            {
                Trace.WriteLine(s);
            });
            _dummyService = dummyService;
            SomeText = _dummyService.What();
        }
    }
}