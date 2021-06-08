﻿using Organizator_Proslava.Dialogs.Custom.Notifications;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.CelebrationRequestForm;
using Organizator_Proslava.ViewModel.Celebrations;
using Organizator_Proslava.ViewModel.UsersView;

namespace Organizator_Proslava.ViewModel
{
    public class MainViewModel : ObservableEntity
    {
        private object _currentViewModel;
        public object CurrentViewModel { get => _currentViewModel; set => OnPropertyChanged(ref _currentViewModel, value); }

        public LoginViewModel Lvm { get; set; }
        public RegisterViewModel Rvm { get; set; }
        public ClientHomeViewModel Chvm { get; set; }
        public OrganizerHomeViewModel Ohvm { get; set; }

        public AdminHomeViewModel Ahvm { get; set; }
        public UsersTableViewModel Utvm { get; set; }
        public CelebrationsTableViewModel CelebrationsTableViewModel { get; set; }

        public MainViewModel(
            LoginViewModel lvm,
            RegisterViewModel rvm,
            ClientHomeViewModel chvm,
            OrganizerHomeViewModel ohvm,
            AdminHomeViewModel ahvm,
            INotificationService ns)
        {
            Lvm = lvm;
            Rvm = rvm;
            Chvm = chvm;
            Ohvm = ohvm;
            Ahvm = ahvm;

            CurrentViewModel = lvm;
            EventBus.RegisterHandler("SwitchMainViewModel", vm => CurrentViewModel = vm);
            EventBus.RegisterHandler("AdminLogin", () => CurrentViewModel = Ahvm);
            EventBus.RegisterHandler("ClientLogin", () => CurrentViewModel = Chvm);
            EventBus.RegisterHandler("OrganizerLogin", () => CurrentViewModel = Ohvm);
            EventBus.RegisterHandler("BackToLogin", () => CurrentViewModel = Lvm);
            EventBus.RegisterHandler("Register", () => CurrentViewModel = Rvm);

            EventBus.RegisterHandler("BackToClientPage", () => CurrentViewModel = Chvm);

            EventBus.RegisterHandler("Notf", () => new DialogService().OpenDialog(new NotificationsDialogViewModel(ns))); // Delete later
        }
    }
}