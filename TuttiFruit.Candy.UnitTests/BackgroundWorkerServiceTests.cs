using AutoFixture;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Interfaces;
using TuttiFruit.Candy.Rabbit.Services;
using Xunit;

namespace TuttiFruit.Candy.UnitTests
{
  public class BackgroundWorkerServiceTests
  {
    private readonly BackgroundWorkerService sut;
    private readonly Mock<IMQSubscriber> _subscriberMock;
    private readonly Mock<IProducer> _producerMock;
    private readonly Mock<IConsumerFactory> _consumerfactoryMock;

    public BackgroundWorkerServiceTests()
    {
      _subscriberMock = new Mock<IMQSubscriber>();
      _producerMock = new Mock<IProducer>();
      _consumerfactoryMock = new Mock<IConsumerFactory>();

      sut = new BackgroundWorkerService(_subscriberMock.Object, _producerMock.Object, _consumerfactoryMock.Object);
    }

    [Fact]
    public async Task CreateBackgroundWorkerService_WhenStart_ShouldCreateConsumers()
    {
      //Act
      await sut.StartAsync(default);

      //Assert
      _consumerfactoryMock.Verify(x => x.CreateConsumers(), Times.Once);
    }

    [Fact]
    public async Task CreateBackgroundWorkerService_WhenStart_ShouldStartConsumers()
    {
      //Arrange
      var consumerMock = new Mock<IConsumer>();
      _consumerfactoryMock.Setup(x => x.CreateConsumers()).Returns(new List<IConsumer>() { consumerMock.Object });

      //Act
      await sut.StartAsync(default);

      //Assert
      consumerMock.Verify(x => x.StartConsumeAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateBackgroundWorkerService_WhenStart_ShouldStartSubscriber()
    {
      //Act
      await sut.StartAsync(default);

      //Assert
      _subscriberMock.Verify(x => x.StartAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task OnMessage_WhenSubscriberNotifyNewMessage_ShouldPublishMessageToChannel()
    {
      //Arrange            
      var message = new Fixture().Create<Message>();

      //Act
      await sut.StartAsync(default);
      _subscriberMock.Raise(x => x.OnMessage += null, new SubscriberEventArgs(message));

      //Assert
      _producerMock.Verify(c => c.PublishMessageAsync(message, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task OnConnectionError_WhenSubscriberNotifyConnectionError_ShouldDisposeSubscriber()
    {
      //Act
      await sut.StartAsync(default);
      _subscriberMock.Raise(x => x.OnConnectionError += null, new ConnectionEventArgs(new Exception()));

      //Assert
      _subscriberMock.Verify(x => x.Dispose(), Times.Once);
    }

    [Fact]
    public async Task OnConnectionError_WhenSubscriberNotifyConnectionError_ShouldStopProducer()
    {
      //Act
      await sut.StartAsync(default);
      _subscriberMock.Raise(x => x.OnConnectionError += null, new ConnectionEventArgs(new Exception()));

      //Assert
      _producerMock.Verify(x => x.Complete(), Times.Once);
    }
  }
}
