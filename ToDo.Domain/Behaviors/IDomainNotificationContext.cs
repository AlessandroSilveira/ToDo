using System.Collections.Generic;

namespace ToDo.Domain.Behaviors
{
    public interface IDomainNotificationContext
    {
        bool HasErrorNotifications { get; }
        public void NotifyError(string message);
        void NotifySuccess(string message);
        List<DomainNotification> GetErrorNotifications();
    }
}