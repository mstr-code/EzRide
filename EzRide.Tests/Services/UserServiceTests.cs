using System;
using System.Threading.Tasks;

using AutoMapper;
using EzRide.Core.Domain;
using EzRide.Core.Repositories;
using EzRide.Infrastructure.DTO;
using EzRide.Infrastructure.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace EzRide.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task successful_user_identification_based_on_existing_user_id()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var mapperMock = new Mock<IMapper>();

            User user = new User(
                Guid.NewGuid(), "user1@domain.com", "user1", "secret", "salt", "user");
            userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(user);
            encrypterMock.Setup(x => x.GetSalt()).Returns("salt");
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("hash");

            IUserService userService =
                new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);
            
            // Act
            // Fetch an existing user.
            await userService.GetAsync(user.Id);

            // Assert
            // Verify that GetAsync was called on any user ID number exactly one time.
            userRepositoryMock.Verify(x => x.GetAsync(It.IsAny<Guid>()), Times.Once);
        }

        [Fact]
        public async Task successful_user_identification_based_on_existing_username()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var mapperMock = new Mock<IMapper>();

            User user = new User(
                Guid.NewGuid(), "user1@domain.com", "user1", "secret", "salt", "user");
            userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(user);
            encrypterMock.Setup(x => x.GetSalt()).Returns("salt");
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("hash");

            IUserService userService =
                new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);
            
            // Act
            // Fetch an existing user.
            await userService.GetAsync(user.Username);      

            // Assert
            // Verify that GetAsync was called on any username string exactly one time.
            userRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task successful_user_registration_based_on_unique_username()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var mapperMock = new Mock<IMapper>();

            encrypterMock.Setup(x => x.GetSalt()).Returns("salt");
            encrypterMock.Setup(x => x.GetHash(It.IsAny<string>(), It.IsAny<string>())).Returns("hash");

            IUserService userService =
                new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);
            
            // Act
            // Register a new user.
            await userService.RegisterAsync(
                Guid.NewGuid(), "user1@domain.com", "user1", "password", "user");

            // Assert
            // Verify that AddAsync was called on any 'user' object exactly one time.
            userRepositoryMock.Verify(x => x.AddAsync(It.IsAny<User>()), Times.Once); 
        }

        [Fact]
        public async Task unsuccessful_user_identification_due_to_nonexisting_username()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var encrypterMock = new Mock<IEncrypter>();
            var mapperMock = new Mock<IMapper>();

            userRepositoryMock.Setup(x => x.GetAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            
            IUserService userService =
                new UserService(userRepositoryMock.Object, encrypterMock.Object, mapperMock.Object);

            // Act
            // Attempt to fetch a non-existing user.
            await userService.GetAsync("user");
            
            // Assert
            // Verify that GetAsync was called on any username string exactly one time.
            userRepositoryMock.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once);
        }
    }
}