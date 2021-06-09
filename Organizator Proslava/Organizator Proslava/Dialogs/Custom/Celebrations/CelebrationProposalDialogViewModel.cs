using Organizator_Proslava.Dialogs.Alert;
using Organizator_Proslava.Dialogs.Custom.Collaborators;
using Organizator_Proslava.Dialogs.Option;
using Organizator_Proslava.Dialogs.Service;
using Organizator_Proslava.Model.CelebrationResponses;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Ninject;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using Organizator_Proslava.ViewModel.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace Organizator_Proslava.Dialogs.Custom.Celebrations
{
    public class CelebrationProposalDialogViewModel : DialogViewModelBase<CelebrationProposal>, IDataErrorInfo
    {
        // Text fields:
        public string ProposalTitle { get; set; }

        public string Content { get; set; }

        private Collaborator _selectedCollaborator;

        public Collaborator SelectedCollaborator
        {
            get => _selectedCollaborator;
            set
            {
                OnPropertyChanged(ref _selectedCollaborator, value);
                Proposal.Collaborator = _selectedCollaborator;
                OnPropertyChanged("ShouldShowHalls");
            }
        }

        public CelebrationProposal Proposal { get; set; }
        public List<Collaborator> Collaborators { get; set; }

        // Commands:
        public ICommand PreviewHall { get; set; }

        public ICommand PreviewCollaborator { get; set; }
        public ICommand Add { get; set; }
        public ICommand Back { get; set; }
        public ICommand OpenCollaboratorsDialog { get; set; }

        public bool ShouldShowHalls { get => Proposal.Collaborator?.CelebrationHalls.Any() ?? false; }

        // Rules:
        public string Error => throw new NotImplementedException();

        public string this[string columnName]
        {
            get
            {
                if (columnName == "SelectedCollaborator")
                    return SelectedCollaborator == null ? "Molimo Vas odaberite saradnika." : null;

                var valueOfProperty = GetType().GetProperty(columnName)?.GetValue(this);
                return Err(ValidationDictionary.Validate(columnName, valueOfProperty, null));
            }
        }

        private readonly ICollaboratorService _collaboratorService;
        private readonly IDialogService _dialogService;
        private int _calls = 0;

        public CelebrationProposalDialogViewModel(ICollaboratorService collaboratorService, IDialogService dialogService, int n) : base("Davanje ponude", 660, 500)
        {
            _collaboratorService = collaboratorService;
            _dialogService = dialogService;

            Proposal = new CelebrationProposal();
            ProposalTitle = $"Ponuda #{n}";
            Collaborators = _collaboratorService.Read().ToList();

            PreviewHall = new RelayCommand(() => _dialogService.OpenDialog(new SpacePreviewDialogViewModel(Proposal.CelebrationHall, _dialogService)), () => Proposal.CelebrationHall != null);

            PreviewCollaborator = new RelayCommand(() =>
            {
                var dcdvm = ServiceLocator.Get<DetailsCollaboratorDialogViewModel>();
                dcdvm.Collaborator = _selectedCollaborator;
                dcdvm.DisplayInfoAboutCollaborator();
                _dialogService.OpenDialog(dcdvm);
            }, () => SelectedCollaborator != null);

            Add = new RelayCommand<IDialogWindow>(w =>
            {
                if (ShouldShowHalls && Proposal.CelebrationHall == null)
                {
                    _dialogService.OpenDialog(new AlertDialogViewModel("Obaveštenje", "Molimo Vas izaberite salu."));
                    return;
                }

                if (_dialogService.OpenDialog(new OptionDialogViewModel("Potvrda", "Da li ste sigurni da želite da date ovaj predlog?")) == DialogResults.Yes)
                {
                    Proposal.Title = ProposalTitle;
                    Proposal.Content = Content;
                    if (Proposal.CelebrationHall != null)
                    {
                        Proposal.CelebrationHall = Proposal.CelebrationHall.Clone();
                        Proposal.CelebrationHall.CollaboratorId = null;
                        Proposal.CelebrationHall.Id = Guid.Empty;
                        foreach (var placeableEntity in Proposal.CelebrationHall.PlaceableEntities)
                            placeableEntity.Id = Guid.Empty;
                    }
                    CloseDialogWithResult(w, Proposal);
                }
            });

            Back = new RelayCommand<IDialogWindow>(w => CloseDialogWithResult(w, null));

            OpenCollaboratorsDialog = new RelayCommand(() =>
            {
                var collaborator = _dialogService.OpenDialog(new ChooseCollaboratorViewModel(Collaborators));
                if (collaborator != null)
                {
                    SelectedCollaborator = collaborator;
                }
            });
        }

        private string Err(string message)
        {
            return message == null ? null : (_calls++ < 3 ? "*" : message);
        }
    }
}