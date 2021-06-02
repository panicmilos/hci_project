using Organizator_Proslava.Data;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;

namespace Organizator_Proslava.Services.Implementations
{
    public class NotificationService : CrudService<Notification>, INotificationService
    {
        private readonly IOrganizerService _organizerService;

        public NotificationService(IOrganizerService organizerService, DatabaseContext context) :
            base(context)
        {
            _organizerService = organizerService;
        }
    }
}