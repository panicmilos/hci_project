using Organizator_Proslava.Model;
using System;
using System.Collections.Generic;

namespace Organizator_Proslava.Services.Contracts
{
    public interface INotificationService : ICrudService<Notification>
    {
        IEnumerable<Notification> ReadFor(Guid forUserId);

        IEnumerable<Notification> ReadFrom(Guid celebrationResponseId);

        Notification DeleteCommentNotification(NewCommentNotification newComment);
    }
}