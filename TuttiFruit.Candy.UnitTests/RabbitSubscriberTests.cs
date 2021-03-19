using AutoFixture;
using FluentAssertions;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Entities;
using TuttiFruit.Candy.Rabbit.Implementations;
using Xunit;

namespace TuttiFruit.Candy.UnitTests
{
    public class RabbitSubscriberTests
    {
        [Fact]
        public void StartAsync_WhenSettingsAreValid_ShouldNotThrowException()
        {
            //Arrange
            var settings = Options.Create(new RabbitSettings());
            var sut = new RabbitSubscriber(settings);

            //Act
            Func<Task> actionResult = async () => await sut.StartAsync(default);


            //Assert
            actionResult.Should().NotThrow();
        }

        [Fact]
        public void SendAckAsync_WhenSettingsAreValid_ShouldNotThrowException()
        {
            //Arrange
            var message = new Fixture().Create<Message>();
            var settings = Options.Create(new RabbitSettings());
            var sut = new RabbitSubscriber(settings);

            //Act
            Func<Task> actionResult = async () => await sut.SendAckAsync(message);


            //Assert
            actionResult.Should().NotThrow();
        }
    }
}
