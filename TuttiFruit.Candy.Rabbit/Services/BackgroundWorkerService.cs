using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Interfaces;

namespace TuttiFruit.Candy.Rabbit.Services
{
  public class BackgroundWorkerService : BackgroundService
  {
    private readonly IMQSubscriber _subscriber;
    private readonly IProducer _producer;
    private readonly IConsumerFactory _consumerFactory;
    private CancellationToken _cancellationToken = default;

    public BackgroundWorkerService(IMQSubscriber subscriber, IProducer producer, IConsumerFactory consumerFactory)
    {
      _subscriber = subscriber;
      _producer = producer;
      _consumerFactory = consumerFactory;

      _subscriber.OnMessage += OnMessage;
      _subscriber.OnConnectionError += OnConnectionError;
    }

    private async Task OnConnectionError(object sender, ConnectionEventArgs args)
    {
      _subscriber.Dispose();
      _producer.Complete();

      await Task.CompletedTask;
    }

    private async Task OnMessage(object sender, SubscriberEventArgs args)
    {
      await _producer.PublishMessageAsync(args.Message, _cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken cancellationToken)
    {
      _cancellationToken = cancellationToken;
      var consumerTasks = _consumerFactory.CreateConsumers().Select(c => c.StartConsumeAsync(cancellationToken));

      await _subscriber.StartAsync(cancellationToken);
      await Task.WhenAll(consumerTasks);
    }
  }
}
