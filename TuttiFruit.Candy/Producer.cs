using System;
using System.Threading.Channels;

namespace TuttiFruit.Candy
{
    internal class Producer
    {
        private readonly ChannelWriter<object> _writer;
        private readonly ISubscriber _subscriber;

        public Guid Id { get; private set; } = Guid.NewGuid();

        public Producer(ChannelWriter<object> writer, ISubscriber subscriber)
        {
            _writer = writer;
            _subscriber = subscriber;
            _subscriber.OnMessage += OnMessage;
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            _writer.WriteAsync(e.Message).GetAwaiter().GetResult();
            Console.WriteLine($"Producer {Id}: Message '{e.Message}' has been published");
        }

        internal void Complete()
        {
            _writer.Complete();
            _subscriber.Close();
        }
    }
}