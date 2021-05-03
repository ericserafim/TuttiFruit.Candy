using System;

namespace TuttiFruit.Candy.Rabbit.Entities
{
  public class SubscriberEventArgs : EventArgs
  {
    public Message Message { get; }

    public SubscriberEventArgs(Message message)
    {
      Message = message;
    }
  }
}
