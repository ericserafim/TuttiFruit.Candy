using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using FluentAssertions;
using TuttiFruit.Candy.Rabbit.Implementations;
using System.Threading.Channels;
using TuttiFruit.Candy.Rabbit.Interfaces;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Exceptions;

namespace TuttiFruit.Candy.UnitTests
{
    public class ConsumerTests
    {
        [Fact]
        public void StartConsumeAsync_WhenReceiveMessage_ShouldProcessMessage()
        {
            //Arrange                        
            var channelReaderMock = new Mock<ChannelReader<Message>>();
            var subscriberMock = new Mock<IMQSubscriber>();
            var messageHandlerMock = new Mock<IMessageHandler>();
            var sut = new Consumer(channelReaderMock.Object, subscriberMock.Object, messageHandlerMock.Object);

            channelReaderMock
                .Setup(x => x.ReadAllAsync(default))
                .Returns(MessageBuilder.Build(1));

            //Act
            Func<Task> actionResult = async () => await sut.StartConsumeAsync(default);

            //Assert
            actionResult.Should().NotThrow();
            channelReaderMock.Verify(x => x.ReadAllAsync(default), Times.Once);
            subscriberMock.Verify(x => x.SendAckAsync(It.IsAny<Message>()), Times.Once);
            messageHandlerMock.Verify(x => x.ProcessMessageAsync(It.IsAny<Message>()), Times.Once);
        }

        [Fact]
        public async Task StartConsumeAsync_WhenMessageHandlerNotHandleException_ShouldThrowUnhandledExceptionAsync()
        {
            //Arrange
            var messageCount = 3;
            var channelReaderMock = new Mock<ChannelReader<Message>>();
            var subscriberMock = new Mock<IMQSubscriber>();
            var messageHandlerMock = new Mock<IMessageHandler>();
            var sut = new Consumer(channelReaderMock.Object, subscriberMock.Object, messageHandlerMock.Object);

            channelReaderMock
                .Setup(x => x.ReadAllAsync(default))
                .Returns(MessageBuilder.Build(messageCount));

            messageHandlerMock
                .Setup(x => x.ProcessMessageAsync(It.Is<Message>(m => m.Raw.Equals(messageCount))))
                .Throws<UnhandledException>();

            //Act
            await sut.StartConsumeAsync(default);

            //Assert            
            channelReaderMock.Verify(x => x.ReadAllAsync(default), Times.Once);
            messageHandlerMock.Verify(x => x.ProcessMessageAsync(It.IsAny<Message>()), Times.Exactly(messageCount));
            subscriberMock.Verify(x => x.SendAckAsync(It.IsAny<Message>()), Times.Exactly(messageCount - 1));
        }
    }
}
