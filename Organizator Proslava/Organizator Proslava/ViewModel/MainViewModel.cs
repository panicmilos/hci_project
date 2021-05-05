using Microsoft.EntityFrameworkCore;
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

        private DbContext _context;
        public User User { get; set; } = new User();
        public ICommand _log { get; set; }
        public ICommand _save { get; set; }

        private readonly IDummyService _dummyService;

        public MainViewModel(DbContext context, IDummyService dummyService)
        {
            _context = context;
            _log = new RelayCommand<User>(u =>
            {
                Trace.WriteLine($"{u.Name} {u.Surname}");
            });

            _save = new RelayCommand<User>(u =>
            {
                u.Id = new Guid();
                _context.Add(u);
                _context.SaveChanges();
            });

            _dummyService = dummyService;
            SomeText = _dummyService.What();
        }
    }
}