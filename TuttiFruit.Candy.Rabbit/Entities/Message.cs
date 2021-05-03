namespace TuttiFruit.Candy.Rabbit.Entities
{
  public struct Message
  {
    public ulong DeliveryTag { get; private set; }

    public object Raw { get; private set; }

    public Message(object raw, ulong deliveryTag)
    {
      DeliveryTag = deliveryTag;
      Raw = raw;
    }
  }
}
