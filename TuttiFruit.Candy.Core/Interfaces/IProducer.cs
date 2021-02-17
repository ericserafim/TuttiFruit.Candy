namespace TuttiFruit.Candy.Core.Interfaces
{
    public interface IProducer
    {
        void SetQueueNameToSubscriber();

        void Stop();
    }
}
