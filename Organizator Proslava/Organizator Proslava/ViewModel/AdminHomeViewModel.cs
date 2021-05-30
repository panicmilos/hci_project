using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Utility;

namespace Organizator_Proslava.ViewModel
{
    public class AdminHomeViewModel
    {
        public ICommand Back { get; set; }
        public ICommand Organizers { get; set; }
        
        public AdminHomeViewModel()
        {
            Back = new RelayCommand(() => EventBus.FireEvent("BackToLogin"));
            Organizers = new RelayCommand(() => EventBus.FireEvent("OrganizersTableView"));
        }
    }
}