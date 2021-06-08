﻿using Organizator_Proslava.Dialogs.Map;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.Utils;
using System.ComponentModel;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CollaboratorForm
{
    public class IndividualCollaboratorInformationsViewModel : BaseObservableEntity, IDataErrorInfo
    {
        public string BackTo { get; set; }

        // Text fields:
        public Collaborator Collaborator { get; set; }

        public Address Address { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalId { get; set; }
        public string JMBG { get; set; }
        public string PhoneNumber { get; set; }
        public string _wholeAddress;
        public string WholeAddress { get => _wholeAddress; set => OnPropertyChanged(ref _wholeAddress, value); }
        public string UserName { get; set; }
        public string MailAddress { get; set; }

        private string _password;
        public string Password { get { return _password; } set { OnPropertyChanged(ref _password, value); OnPropertyChanged("RepeatedPassword"); } }

        private string _repeatedPassword;
        public string RepeatedPassword { get { return _repeatedPassword; } set { OnPropertyChanged(ref _repeatedPassword, value); OnPropertyChanged("Password"); } }

        // Validation
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Password" || columnName == "RepeatedPassword")
                    return Err(ValidationDictionary.Validate(columnName, Password, RepeatedPassword));

                var valueOfProperty = GetType().GetProperty(columnName)?.GetValue(this);
                return Err(ValidationDictionary.Validate(columnName, valueOfProperty, null));
            }
        }

        public ICommand OpenMap { get; set; }
        public ICommand Back { get; set; }
        public ICommand Next { get; set; }

        public string Error => throw new System.NotImplementedException();

        private readonly IDialogService _dialogService;
        private int _calls = 0;

        public IndividualCollaboratorInformationsViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            Collaborator = new IndividualCollaborator();

            Back = new RelayCommand(() => EventBus.FireEvent("BackToSelectCollaboratorType"));

            OpenMap = new RelayCommand(() =>
            {
                var address = _dialogService.OpenDialog(new MapDialogViewModel("Odaberi svoju adresu"));
                if (address != null)
                {
                    Address = address;
                    WholeAddress = address?.WholeAddress;
                }
            });

            Next = new RelayCommand(() => EventBus.FireEvent("NextToCollaboratorServicesFromIndividual"));
        }

        public void ForAdd()
        {
            Collaborator = new IndividualCollaborator();
            foreach (var property in Collaborator.GetType().GetProperties())
            {
                GetType().GetProperty(property.Name)?.SetValue(this, property.GetValue(Collaborator));
            }
            WholeAddress = string.Empty;
            RepeatedPassword = string.Empty;
            _calls = 0;

            Back = new RelayCommand(() => EventBus.FireEvent("BackToSelectCollaboratorType"));
        }

        public void ForUpdate(Collaborator collaborator)
        {
            Collaborator = collaborator;
            foreach (var property in collaborator.GetType().GetProperties())
            {
                GetType().GetProperty(property.Name)?.SetValue(this, property.GetValue(collaborator));
            }
            WholeAddress = Collaborator.Address?.WholeAddress;
            RepeatedPassword = collaborator.Password;
            _calls = 10;

            Back = new RelayCommand(() => EventBus.FireEvent(BackTo));
        }

        public Collaborator CollectCollaborator()
        {
            foreach (var property in GetType().GetProperties())
            {
                Collaborator.GetType().GetProperty(property.Name)?.SetValue(Collaborator, property.GetValue(this));
            }

            return Collaborator;
        }

        private string Err(string message)
        {
            return message == null ? null : (_calls++ < 10 ? "*" : message);   // there are 7 fields
        }
    }
}