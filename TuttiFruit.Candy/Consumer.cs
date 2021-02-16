using System;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace TuttiFruit.Candy
{
    internal class Consumer
    {
        private ChannelReader<object> _reader;

        public Guid Id { get; private set; } = Guid.NewGuid();

        public Consumer(ChannelReader<object> reader)
        {
            _reader = reader;
        }

        public async Task StartConsumeAsync()
        {
            await foreach (var message in _reader.ReadAllAsync())
            {
                Console.WriteLine($"Consumer => {message}");
            }
        }
    }
}