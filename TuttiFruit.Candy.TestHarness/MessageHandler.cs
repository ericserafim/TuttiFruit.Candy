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
            throw new NotImplementedException();
        }
    }
}
