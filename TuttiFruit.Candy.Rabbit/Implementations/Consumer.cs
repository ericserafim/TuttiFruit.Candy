using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Exceptions;
using TuttiFruit.Candy.Rabbit.Interfaces;

namespace TuttiFruit.Candy.Rabbit.Implementations
{
  public sealed class Consumer : IConsumer
  {
    private ChannelReader<Message> _channelReader;
    private IMQSubscriber _subscriber;
    private IMessageHandler _messageHandler;

    public Consumer(
        ChannelReader<Message> channelReader,
        IMQSubscriber mQSubscriber,
        IMessageHandler messageHandler)
    {
      _channelReader = channelReader;
      _subscriber = mQSubscriber;
      _messageHandler = messageHandler;
    }

    public async Task StartConsumeAsync(CancellationToken cancellationToken)
    {
      await foreach (var message in _channelReader.ReadAllAsync(cancellationToken))
      {
        try
        {
          await _messageHandler.ProcessMessageAsync(message);
          await _subscriber.SendAckAsync(message);
        }
        catch (Exception e)
        {
          //TODO Exception should be logged 
          await Task.FromException(e);
        }
      }
    }
  }
}
