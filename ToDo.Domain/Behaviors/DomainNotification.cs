namespace ToDo.Domain.Behaviors
{
    public class DomainNotification
    {
        public DomainNotificationType Type { get; protected set; }
        public string Value { get; protected set; }

        public DomainNotification(DomainNotificationType type, string value)
        {
            Type = type;
            Value = value;
        }
    }
}