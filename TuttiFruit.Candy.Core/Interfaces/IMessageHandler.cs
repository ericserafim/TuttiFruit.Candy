using System.Threading.Tasks;
using TuttiFruit.Candy.Core.Entities;

namespace TuttiFruit.Candy.Core.Interfaces
{
    public interface IMessageHandler
    {
        Task ProcessMessageAsync(Message message);
    }
}
