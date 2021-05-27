﻿using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Celebrations
{
    public class CelebrationProposalDialogViewModel : DialogViewModelBase<CelebrationProposal>
    {
        public CelebrationProposal Proposal { get; set; }
        public List<Collaborator> Collaborators { get; set; }

        public ICommand Add { get; set; }
        public ICommand Back { get; set; }

        private readonly ICollaboratorService _collaboratorService;
        private readonly IDialogService _dialogService;

        public CelebrationProposalDialogViewModel(ICollaboratorService collaboratorService, IDialogService dialogService) : base("Davanje ponude", 660, 500)
        {
            _collaboratorService = collaboratorService;
            _dialogService = dialogService;

            Proposal = new CelebrationProposal();
            Collaborators = _collaboratorService.Read().ToList();

            Add = new RelayCommand<IDialogWindow>(w =>
            {
                if (_dialogService.OpenDialog(new OptionDialogViewModel("Pitanje", "Da li ste sigurni da želite da date ovaj predlog?")) == DialogResults.Yes)
                {
                    CloseDialogWithResult(w, Proposal);
                }
            });
            Back = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, null));
        }
    }
}