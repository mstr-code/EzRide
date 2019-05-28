using System;
using System.Threading.Tasks;
using AutoMapper;
using EzRide.Core.Domain;
using EzRide.Core.Repositories;
using EzRide.Infrastructure.DTO;
using EzRide.Infrastructure.Services;
using Moq;
using Xunit;

namespace EzRide.Tests.Services
{
    public class DriverServiceTests
    {
        [Fact]
        public async Task successful_driver_identification_based_on_existing_user_id()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var driverRepositoryMock = new Mock<IDriverRepository>();
            var mapperMock = new Mock<IMapper>();

            User user = new User(
                Guid.NewGuid(), "user1@domain.com", "user1", "secret", "salt", "user");   
            Vehicle vehicle = Vehicle.Create("Audi", "A1", "White", 4);
            Driver driver = new Driver(user, vehicle);
            driverRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(driver);
            
            IDriverService driverService = new DriverService(driverRepositoryMock.Object,
                userRepositoryMock.Object, mapperMock.Object);

            // Act
            // Fetch an existing driver.
            await driverService.GetAsync(user.Id);

            // Assert
            // Verify that GetAsync was called on any user ID number exactly one time.
            driverRepositoryMock.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task successful_driver_identification_based_on_existing_username()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var driverRepositoryMock = new Mock<IDriverRepository>();
            var mapperMock = new Mock<IMapper>();

            User user = new User(
                Guid.NewGuid(), "user1@domain.com", "user1", "secret", "salt", "user");   
            Vehicle vehicle = Vehicle.Create("Audi", "A1", "White", 4);
            Driver driver = new Driver(user, vehicle);
            driverRepositoryMock.Setup(x => x.GetAsync(It.IsAny<Guid>())).ReturnsAsync(driver);
            
            IDriverService driverService = new DriverService(driverRepositoryMock.Object,
                userRepositoryMock.Object, mapperMock.Object);

            // Act
            // Fetch an existing driver.
            await driverService.GetAsync(user.Username);

            // Assert
            // Verify taht GetAsync was called on any username string exactly one time.
            driverRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);
        } 
    }
}