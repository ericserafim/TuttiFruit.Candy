using System;

namespace TuttiFruit.Candy
{
    internal class IBMSubscriber : ISubscriber
    {
        public event EventHandler<MessageEventArgs> OnMessage;

        public void PublishMessage(string message)
        {
            OnMessage(this, new MessageEventArgs(message));
        }

        public void Close()
        {
            Console.WriteLine($"{nameof(IBMSubscriber)} has been closed.");
        }
    }
}