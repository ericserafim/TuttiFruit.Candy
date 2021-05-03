using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruitHandlers = TuttiFruit.Candy.Rabbit.Handlers;
using TuttiFruit.Candy.Rabbit.Interfaces;

namespace TuttiFruit.Candy.Rabbit.Implementations
{
  public class RabbitSubscriber : IMQSubscriber
  {
    private readonly RabbitSettings _rabbitSettings;

    private IConnection _connection;

    private IModel _channel;

    public event TuttiFruitHandlers.AsyncEventHandler<SubscriberEventArgs> OnMessage;

    public event TuttiFruitHandlers.AsyncEventHandler<ConnectionEventArgs> OnConnectionError;    

    public RabbitSubscriber(IOptions<RabbitSettings> rabbitSettings)
    {
      _rabbitSettings = rabbitSettings.Value;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
      await Connect();
    }

    private async Task Connect()
    {
      try
      {
        var factory = new ConnectionFactory();

        factory.UserName = _rabbitSettings.UserName;
        factory.Password = _rabbitSettings.Password;
        factory.VirtualHost = _rabbitSettings.VirtualHost;
        factory.HostName = _rabbitSettings.HostName;
        factory.Port = _rabbitSettings.Port;

        factory.Ssl = new SslOption
        {
          Enabled = _rabbitSettings.SslEnabled,
          CertPath = _rabbitSettings.SslCertPath,
          ServerName = _rabbitSettings.SslServerName,
          Version = _rabbitSettings.SslVersion
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueBind(_rabbitSettings.QueueName, exchange: string.Empty, routingKey: string.Empty);

        SetOnMessageReceived();

        await Task.CompletedTask;
      }
      catch (Exception e)
      {
        OnConnectionError?.Invoke(this, new ConnectionEventArgs(e));
      }
    }

    private void SetOnMessageReceived()
    {
      var consumer = new EventingBasicConsumer(_channel);

      consumer.Received += async (model, args) =>
      {
        var body = args.Body.ToArray();
        var payload = Encoding.UTF8.GetString(body);        
        await OnMessage?.Invoke(this, new SubscriberEventArgs(new Message(payload, args.DeliveryTag)));
      };

      _channel.BasicConsume(_rabbitSettings.QueueName, autoAck: false, consumer);
    }

    public Task SendAckAsync(Message message)
    {
      try
      {
        _channel.BasicAck(message.DeliveryTag, multiple: false);
        return Task.CompletedTask;
      }
      catch (Exception e)
      {
        //TODO log error message. 
        
        //TODO Check if the default behavior will be requeue = true
        //Requeue can become a DDOS against (App, PCF and/or Log players).
        _channel.BasicReject(message.DeliveryTag, requeue: true);

        return Task.FromException(e);
      }
    }

    public void Dispose()
    {
      _channel?.Close();
      _connection?.Close();

      _channel.Dispose();
      _connection?.Dispose();
    }
  }
}
