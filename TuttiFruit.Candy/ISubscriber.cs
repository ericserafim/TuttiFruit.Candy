using System;

namespace TuttiFruit.Candy
{
    public interface ISubscriber
    {
        event EventHandler<MessageEventArgs> OnMessage;

        void PublishMessage(string message);
        void Close();
    }
}