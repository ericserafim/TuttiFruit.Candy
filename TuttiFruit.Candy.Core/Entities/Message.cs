namespace TuttiFruit.Candy.Core.Entities
{
    public struct Message
    {
        public string SubscriberType { get; private set; }

        public object Raw { get; private set; }

        public Message(string subscriberType, object raw)
        {
            SubscriberType = subscriberType;
            Raw = raw;
        }
    }
}
