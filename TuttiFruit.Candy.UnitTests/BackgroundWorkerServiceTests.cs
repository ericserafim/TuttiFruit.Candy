using Moq;
using System;
using System.Threading.Tasks;
using TuttiFruit.Candy.Rabbit.Interfaces;
using TuttiFruit.Candy.Rabbit.Services;
using Xunit;

namespace TuttiFruit.Candy.UnitTests
{
    public class BackgroundWorkerServiceTests
    {
        [Fact]
        public async Task CreateBackgroundWorkerService_WhenStart_ShouldCreateConsumers()
        {
            //Arrange            
            var consumerfactoryMock = new Mock<IConsumerFactory>();
            var sut = new BackgroundWorkerService(new Mock<IMQSubscriber>().Object, new Mock<IProducer>().Object, consumerfactoryMock.Object);

            //Act
            await sut.StartAsync(default);

            //Assert
            consumerfactoryMock.Verify(x => x.CreateConsumers(), Times.Once);
        }
    }
}
