namespace ArbanFramework.MVC
{
    public class EventBase
    {
        public EventTypeBase eventType { get; private set; }
        public object sender { get; private set; }

        public EventBase(EventTypeBase eventType, object sender)
        {
            this.eventType = eventType;
            this.sender = sender;
        }
    }
}