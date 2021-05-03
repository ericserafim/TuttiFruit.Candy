using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using System.Threading.Channels;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Implementations;
using Xunit;

namespace TuttiFruit.Candy.UnitTests
{
  public class ProducerTests
  {
    [Fact]
    public async Task PublishMessageAsync_WhenReceiveMessage_ShouldPublishToChannelAsync()
    {
      //Arrange
      var message = new Fixture().Create<Message>();
      var loggerMock = new Mock<ILogger<Producer>>();
      var channelWriterMock = new Mock<ChannelWriter<Message>>();
      var sut = new Producer(channelWriterMock.Object, loggerMock.Object);

      //Act
      await sut.PublishMessageAsync(message, default);

      //Assert
      channelWriterMock.Verify(x => x.WriteAsync(message, default), Times.Once);
    }

    [Fact]
    public void Complete_WhenProducerComplete_ShouldCompleteChannel()
    {
      //Arrange            
      var loggerMock = new Mock<ILogger<Producer>>();
      var channelWriterMock = new Mock<ChannelWriter<Message>>();
      var sut = new Producer(channelWriterMock.Object, loggerMock.Object);

      //Act
      sut.Complete();

      //Assert
      channelWriterMock.Verify(x => x.TryComplete(default), Times.Once);
    }
  }
}
