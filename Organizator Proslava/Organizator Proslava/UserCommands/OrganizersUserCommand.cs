using Organizator_Proslava.Model;
using Organizator_Proslava.Model.Cellebrations;
using Organizator_Proslava.Ninject;
using Organizator_Proslava.Services.Contracts;
using Organizator_Proslava.Utility;

namespace Organizator_Proslava.UserCommands
{
    public class CreateOrganizer : IUserCommand
    {
        private readonly Organizer _organizer;
        private readonly Address _address;
        private readonly CellebrationType _cellebrationType;

        private readonly IOrganizerService _organizerService;

        public CreateOrganizer(Organizer organizer)
        {
            _organizer = organizer;
            _address = organizer.Address;
            _cellebrationType = organizer.CellebrationType;

            _organizerService = ServiceLocator.Get<IOrganizerService>();
        }

        public void Redo()
        {
            _organizer.AddressId = _address.Id;
            _organizer.Address = null;
            _organizer.CellebrationTypeId = _cellebrationType.Id;
            _organizer.CellebrationType = null;
            _organizerService.Create(_organizer);
            EventBus.FireEvent("ReloadOrganizerTable");
        }

        public void Undo()
        {
            _organizerService.Delete(_organizer.Id);
            EventBus.FireEvent("ReloadOrganizerTable");
        }
    }

    public class UpdateOrganizer : IUserCommand
    {
        private readonly Organizer _currentOrganizer;
        private readonly Organizer _newOrganizer;

        private readonly IOrganizerService _organizerService;

        public UpdateOrganizer(Organizer currentOrganizer, Organizer newOrganizer)
        {
            _currentOrganizer = currentOrganizer;
            _newOrganizer = newOrganizer;

            _organizerService = ServiceLocator.Get<IOrganizerService>();
        }

        public void Redo()
        {
            _organizerService.Update(_newOrganizer);
            EventBus.FireEvent("ReloadOrganizerTable");
        }

        public void Undo()
        {
            _organizerService.Update(_currentOrganizer);
            EventBus.FireEvent("ReloadOrganizerTable");
        }
    }

    public class DeleteOrganizer : IUserCommand
    {
        private readonly IUserCommand _createCommand;

        public DeleteOrganizer(Organizer organizer)
        {
            _createCommand = new CreateOrganizer(organizer);
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