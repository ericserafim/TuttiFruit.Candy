using System;
using System.Threading.Tasks;
using TuttiFruit.Candy.Core.Entities;
using TuttiFruit.Candy.Core.Interfaces;

namespace TuttiFruit.Candy.TestHarness
{
    public class MessageHandler : IMessageHandler
    {
        private readonly Func<string, IMqSubscriber> _subscriberGetter;

        public MessageHandler(Func<string, IMqSubscriber> subscriberGetter)
        {
            this._subscriberGetter = subscriberGetter;
        }

        public async Task ProcessMessageAsync(Message message)
        {
            var subscriber = _subscriberGetter(message.SubscriberType);
            
            //Simulation
            Console.WriteLine($"'Message from '{message.SubscriberType}' has been processed.");
            await Task.Delay(1000);

            await subscriber.SendAckAsync(message.Raw);
        }
    }
}
