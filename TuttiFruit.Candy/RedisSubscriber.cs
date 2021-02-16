using System;

namespace TuttiFruit.Candy
{
    internal class RedisSubscriber : ISubscriber
    {
        public event EventHandler<MessageEventArgs> OnMessage;

        public void PublishMessage(string message)
        {
            OnMessage(this, new MessageEventArgs(message));
        }

        public void Close()
        {
            Console.WriteLine($"{nameof(RedisSubscriber)} has been closed.");
        }
    }
}