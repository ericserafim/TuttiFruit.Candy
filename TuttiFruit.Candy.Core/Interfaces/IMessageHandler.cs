using System.Threading.Tasks;

namespace TuttiFruit.Candy.Core.Interfaces
{
    public interface IMessageHandler
    {
        Task ProcessMessageAsync(object message);
    }
}
