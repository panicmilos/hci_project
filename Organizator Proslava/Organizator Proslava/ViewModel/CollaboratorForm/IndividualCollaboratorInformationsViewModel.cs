﻿using Organizator_Proslava.Dialogs.Map;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Utility;
using System.Windows.Input;

namespace Organizator_Proslava.ViewModel.CollaboratorForm
{
    public class IndividualCollaboratorInformationsViewModel
    {
        public Collaborator Collaborator { get; set; }

        public ICommand OpenMap { get; set; }
        public ICommand Back { get; set; }
        public ICommand Next { get; set; }

        private IDialogService _dialogService;

        public IndividualCollaboratorInformationsViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            Collaborator = new IndividualCollaborator();

            Back = new RelayCommand(() => EventBus.FireEvent("BackToSelectCollaboratorType"));

            OpenMap = new RelayCommand(() =>
            {
                var address = _dialogService.OpenDialog(new MapDialogViewModel("Odaberi svoju adresu"));
                Collaborator.Address = address;
            });

            Next = new RelayCommand(() => EventBus.FireEvent("NextToCollaboratorServicesFromIndividual"));
        }
    }
}