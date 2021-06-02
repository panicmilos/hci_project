using Organizator_Proslava.Data;
using Organizator_Proslava.Model;
using Organizator_Proslava.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

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

        public override Notification Create(Notification notification)
        {
            return base.Create(notification);
        }

        public IEnumerable<Notification> ReadFor(Guid forUserId)
        {
            return _entities.Where(n => n.ForUserId == forUserId).ToList();
        }
    }
}