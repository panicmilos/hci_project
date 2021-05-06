using Microsoft.EntityFrameworkCore;
using Organizator_Proslava.Utility;
using System;

namespace Organizator_Proslava.ViewModel
{
    public class MainViewModel : ObservableEntity
    {
        public String SomeText { get; set; }

        private DbContext _context;

        private object _currentViewModel;
        public object CurrentViewModel { get => _currentViewModel; set => OnPropertyChanged(ref _currentViewModel, value); }

        public LoginViewModel Lvm { get; set; }
        public RegisterViewModel Rvm { get; set; }

        public MainViewModel(DbContext context)
        {
            _context = context;

            Lvm = new LoginViewModel();
            Rvm = new RegisterViewModel();

            CurrentViewModel = Lvm;
            EventBus.RegisterHandler("Register", () => CurrentViewModel = Rvm);
        }
    }
}