using System.Collections.Generic;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;
using AutoFixture;

namespace TuttiFruit.Candy.UnitTests
{
    internal static class MessageBuilder
    {
        public static async IAsyncEnumerable<Message> Build(int numberOfMessages)
        {
            var messages = new List<Message>();            

            for (int i = 1; i <= numberOfMessages; i++)
            {
                messages.Add(new Message(raw: i));
            }

            foreach (var message in messages)
            {
                yield return message;
            }

            await Task.CompletedTask;
        }
    }
}
