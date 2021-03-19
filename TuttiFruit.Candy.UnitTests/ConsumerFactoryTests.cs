using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Factories;
using TuttiFruit.Candy.Rabbit.Interfaces;
using Xunit;

namespace TuttiFruit.Candy.UnitTests
{
    public class ConsumerFactoryTests
    {
        [Fact]
        public void CreateConsumers_WhenSettingsIsValid_ShouldCreateConsumers()
        {
            //Arrange
            var numberOfConsumers = 3;
            var channelReaderMock = new Mock<ChannelReader<Message>>();
            var subscriberMock = new Mock<IMQSubscriber>();
            Func<IMessageHandler> messageHandlerGetterMock = () => new Mock<IMessageHandler>().Object;
            var settings = Options.Create(new TuttiFruitCandySettings { NumberOfConsumers = numberOfConsumers });            
            var sut = new ConsumerFactory(settings, channelReaderMock.Object, subscriberMock.Object, messageHandlerGetterMock);

            //Act
            var actionResult = sut.CreateConsumers();

            //Assert
            actionResult.Count().Should().Be(numberOfConsumers);
        }
    }
}
