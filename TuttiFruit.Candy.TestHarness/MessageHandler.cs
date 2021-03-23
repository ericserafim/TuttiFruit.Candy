using System;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Interfaces;

namespace TuttiFruit.Candy.TestHarness
{
    public class MessageHandler : IMessageHandler
    {             
        public async Task ProcessMessageAsync(Message message)
        {
            Console.WriteLine($"Message has been received {message.Raw}");
            await Task.CompletedTask;
        }
    }
}
