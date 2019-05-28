using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using EzRide.Api;
using EzRide.Infrastructure.Commands.Users;
using EzRide.Infrastructure.DTO;
using EzRide.Tests.EndToEnd.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Xunit;

namespace EzRide.Tests.EndToEnd
{
    public class UsersControllerTests : ControllerTestsBase
    {
        public UsersControllerTests() : base() { }

        [Fact]
        public async Task successful_user_identification_based_on_existing_username()
        {
            // Arrange
            string username = "user1";

            // Act
            HttpResponseMessage response = await Client.GetAsync($"users/{username}");
            
            // Assert
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            // Act
            UserDto user = await GetDeserializedPayload<UserDto>(response);

            // Assert
            Assert.Equal(user.Username, username);
            // user.Email.Should().BeEquivalentTo(email);
        }

        [Fact]
        public async Task unsuccessful_user_identification_due_to_nonexisting_username()
        {
            // Arrange
            string username = "user9999";

            // Act
            HttpResponseMessage response = await Client.GetAsync($"users/{username}");

            // Assert
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NotFound);
        }

        [Fact]
        public async Task successful_user_registration_based_on_unique_username()
        {
            // Arrange
            CreateUser request = new CreateUser
            {
                Id = Guid.NewGuid(),
                Username = "username",
                Role = "user",
                Email = "user@domain.com",
                Password = "secret"
            };
            
            // Act
            HttpContent payload = GetSerializedPayload(request);
            // Send POST request with the specified payload to server; path 'users/'.
            HttpResponseMessage response = await Client.PostAsync("users", payload);

            // Assert
            // Verify that the HTTP Post request was successful.
            // (HTTP response status code 201: Created)
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);
            // Vefify that the URI of the registered user.
            response.Headers.Location.ToString()
                .Should().BeEquivalentTo($"users/{request.Username}");

            // Act
            // Fetch a user with the specified email address.
            UserDto user = await GetUserAsync(request.Username);

            // Assert
            // Verify that the user's email address is correct.
            user.Email.Should().BeEquivalentTo(request.Username);
        }

        private async Task<UserDto> GetUserAsync(string username)
        {
            // Send GET request with the specified user's email address.
            HttpResponseMessage response = await Client.GetAsync($"users/{username}");

            // Verify that the HTTP Get request was successful.
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            // Return deserialized user object retrieved from the response message.
            return await GetDeserializedPayload<UserDto>(response);
        }
    }
}
