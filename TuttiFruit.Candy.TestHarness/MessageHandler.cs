using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuttiFruit.Candy.Core.Interfaces;

namespace TuttiFruit.Candy.TestHarness
{
    public class MessageHandler : IMessageHandler
    {
        public Task ProcessMessageAsync(object message)
        {
            Console.WriteLine($"'{message}' has been processed.");
            return Task.CompletedTask;
        }
    }
}
