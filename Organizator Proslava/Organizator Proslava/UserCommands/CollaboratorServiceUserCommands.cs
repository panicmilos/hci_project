using Organizator_Proslava.Model.Collaborators;
using System.Collections.ObjectModel;

namespace Organizator_Proslava.UserCommands
{
    public class CreateService : IUserCommand
    {
        private readonly CollaboratorService _collaboratorService;
        private readonly ObservableCollection<CollaboratorService> _services;
        private readonly CollaboratorServiceBook _collaboratorServiceBook;

        public CreateService(CollaboratorService collaboratorService, ObservableCollection<CollaboratorService> services, CollaboratorServiceBook collaboratorServiceBook)
        {
            _collaboratorService = collaboratorService;
            _services = services;
            _collaboratorServiceBook = collaboratorServiceBook;
        }

        public void Redo()
        {
            _services.Add(_collaboratorService);
            _collaboratorServiceBook.Services.Add(_collaboratorService);
        }

        public void Undo()
        {
            _services.Remove(_collaboratorService);
            _collaboratorServiceBook.Services.Remove(_collaboratorService);
        }
    }

    public class UpdateService : IUserCommand
    {
        private readonly CollaboratorService _collaboratorService;
        private readonly CollaboratorService _oldCollaboratorService;
        private readonly CollaboratorService _newCollaboratorService;

        public UpdateService(CollaboratorService collaboratorService, CollaboratorService oldCollaboratorService, CollaboratorService newCollaboratorService)
        {
            _collaboratorService = collaboratorService;
            _oldCollaboratorService = oldCollaboratorService;
            _newCollaboratorService = newCollaboratorService;
        }

        public void Redo()
        {
            _collaboratorService.Name = _newCollaboratorService.Name;
            _collaboratorService.Price = _newCollaboratorService.Price;
            _collaboratorService.Unit = _newCollaboratorService.Unit;
        }

        public void Undo()
        {
            _collaboratorService.Name = _oldCollaboratorService.Name;
            _collaboratorService.Price = _oldCollaboratorService.Price;
            _collaboratorService.Unit = _oldCollaboratorService.Unit;
        }
    }

    public class DeleteService : IUserCommand
    {
        private readonly IUserCommand _createCommand;

        public DeleteService(CollaboratorService collaboratorService, ObservableCollection<CollaboratorService> services, CollaboratorServiceBook collaboratorServiceBook)
        {
            _createCommand = new CreateService(collaboratorService, services, collaboratorServiceBook);
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