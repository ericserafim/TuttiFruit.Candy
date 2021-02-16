using System;

namespace TuttiFruit.Candy
{
    internal class KafkaSubscriber : ISubscriber
    {
        public event EventHandler<MessageEventArgs> OnMessage;

        public void Close()
        {
            Console.WriteLine($"{nameof(KafkaSubscriber)} has been closed.");
        }

        public void PublishMessage(string message)
        {
            OnMessage(this, new MessageEventArgs(message));
        }
    }
}