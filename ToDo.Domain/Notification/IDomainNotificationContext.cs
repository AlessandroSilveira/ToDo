using System.Collections.Generic;

namespace ToDo.Domain.Notification
{
    public interface IDomainNotificationContext
    {
        bool HasErrorNotifications { get; }
        void NotifyError(string message);
        void NotifySuccess(string message);
        List<DomainNotification> GetErrorNotifications();
    }
}