using Organizator_Proslava.Model;
using Organizator_Proslava.Model.Collaborators;
using Organizator_Proslava.Ninject;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;
using System.Diagnostics;
using System.Linq;

namespace Organizator_Proslava.UserCommands
{
    public class CreateCollaborator : IUserCommand
    {
        private readonly Collaborator _collaborator;
        private readonly Address _address;

        private readonly ICollaboratorService _collaboratorService;

        public CreateCollaborator(Collaborator collaborator)
        {
            _collaborator = collaborator;
            _address = collaborator.Address;

            _collaboratorService = ServiceLocator.Get<ICollaboratorService>();
        }

        public void Redo()
        {
            _collaborator.AddressId = _address.Id;
            _collaborator.Address = null;

            _collaboratorService.Create(_collaborator);
            EventBus.FireEvent("ReloadCollaboratorTable");
        }

        public void Undo()
        {
            _collaboratorService.Delete(_collaborator.Id);
            EventBus.FireEvent("ReloadCollaboratorTable");
        }
    }

    public class UpdateCollaborator : IUserCommand
    {
        private readonly Collaborator _currentCollaborator;
        private readonly Collaborator _newCollaborator;

        private readonly ICollaboratorService _collaboratorService;

        public UpdateCollaborator(Collaborator currentCollaborator, Collaborator newCollaborator)
        {
            _currentCollaborator = currentCollaborator;
            _newCollaborator = newCollaborator;

            _collaboratorService = ServiceLocator.Get<ICollaboratorService>();
        }

        public void Redo()
        {
            _collaboratorService.Update(_newCollaborator.Clone());
            EventBus.FireEvent("ReloadCollaboratorTable");
        }

        public void Undo()
        {
            _collaboratorService.Update(_currentCollaborator.Clone());
            EventBus.FireEvent("ReloadCollaboratorTable");
        }
    }

    public class DeleteCollaborator : IUserCommand
    {
        private readonly IUserCommand _createCommand;

        public DeleteCollaborator(Collaborator collaborator)
        {
            _createCommand = new CreateCollaborator(collaborator);
        }

        public void Redo()
        {
            _createCommand.Undo();
        }

        public void Undo()
        {
            _createCommand.Redo();
        }
    }
}