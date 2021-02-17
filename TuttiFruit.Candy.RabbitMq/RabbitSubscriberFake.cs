using System;
using System.Threading.Tasks;
using TuttiFruit.Candy.Core.Entities;
using TuttiFruit.Candy.Core.Handlers;
using TuttiFruit.Candy.Core.Interfaces;

namespace TuttiFruit.Candy.RabbitMq
{
    public class RabbitSubscriberFake : IMqSubscriber
    {
        public event AsyncEventHandler<SubscriberEventArgs> OnMessage;


        public RabbitSubscriberFake()
        {
            FireAndForget();
        }

        private void FireAndForget()
        {
            Task.Run(async () =>
            {
                while (true)
                {
                    if (OnMessage != null)
                        await OnMessage(this, new SubscriberEventArgs($"Message from {nameof(RabbitSubscriberFake)}"));

                    await Task.Delay(5000);
                }
            });
        }

        public Task SendAckAsync(object message)
        {
            Console.WriteLine($"{nameof(RabbitSubscriberFake)}.{nameof(SendAckAsync)}");

            return Task.CompletedTask;
        }
        public void Dispose()
        {
            Console.WriteLine($"{nameof(RabbitSubscriberFake)} closed.");
        }

        public void SetListenningTo(string queueName)
        {
            Console.WriteLine($"{nameof(RabbitSubscriberFake)} listenning to {queueName}");
        }
    }
}
