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
        public NotificationService(DatabaseContext context) :
            base(context)
        {
        }

        public override Notification Create(Notification notification)
        {
            if (notification is NewCommentNotification commentNotification)
            {
                var existingNewComment = _context.NewCommentNotifications
                    .FirstOrDefault(cn => cn.ForUserId == commentNotification.ForUserId &&
                                          cn.CelebrationResponseId == commentNotification.CelebrationResponseId &&
                                          cn.ProposalId == commentNotification.ProposalId);

                if (existingNewComment != null)
                {
                    existingNewComment.NumOfComments++;
                    base.Update(existingNewComment);

                    return existingNewComment;
                }
            }

            return base.Create(notification);
        }

        public IEnumerable<Notification> ReadFor(Guid forUserId)
        {
            return _entities.Where(n => n.ForUserId == forUserId).ToList();
        }

        public Notification DeleteCommentNotification(NewCommentNotification newComment)
        {
            if (newComment.NumOfComments == 1)
            {
                Delete(newComment.Id);
                return newComment;
            }

            newComment.NumOfComments -= 1;
            return base.Update(newComment);
        }
    }
}