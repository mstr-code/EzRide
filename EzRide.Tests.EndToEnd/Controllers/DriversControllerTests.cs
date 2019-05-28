using System.Net.Http;
using System.Threading.Tasks;

using FluentAssertions;
using EzRide.Infrastructure.Services;
using System.Net;
using EzRide.Infrastructure.DTO;
using Xunit;
using EzRide.Core.Domain;
using EzRide.Infrastructure.Commands.Drivers;
using System;

namespace EzRide.Tests.EndToEnd.Controllers
{
    public class DriversControllerTests : ControllerTestsBase
    {
        public DriversControllerTests() : base() { }

        [Fact]
        public async Task successful_driver_registration_based_on_username_and_user_id()
        {
            // Arrange
            string username = "user1";

            // Act
            HttpResponseMessage userResponse = await Client.GetAsync($"users/{username}");
            
            // Assert
            userResponse.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            // Act
            UserDto user = await GetDeserializedPayload<UserDto>(userResponse);

            // Assert
            Assert.Equal(user.Username, username);

            // Arrange
            CreateDriver.DriverVehicle vehicle = new CreateDriver.DriverVehicle
            {
                Brand = "Audi",
                Model = "A1",
                Color = "White",
                SeatingCapacity = 4
            };
            CreateDriver request = new CreateDriver
            {
                UserId = user.Id,
                Vehicle = vehicle
            };

            // Act
            HttpContent payload = GetSerializedPayload(request);
            // Send POST request with the specified payload to server; path 'drivers/'.
            HttpResponseMessage response = await Client.PostAsync("drivers", payload);

            // Assert
            // Verify that the HTTP Post request was successful.
            // (HTTP response status code 201: Created)
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.Created);
            // Vefify that the URI of the registered user.
            response.Headers.Location.ToString()
                .Should().BeEquivalentTo($"drivers/{request.UserId}");
            
            // Act
            // Fetch a user with the specified email address.
            DriverDto driver = await GetUserAsync(request.UserId);

            // Assert
            // Verify that the user's email address is correct.
            driver.UserId.Should().Equals(request.UserId);
        }

        async Task<DriverDto> GetUserAsync(Guid userId)
        {
            // Send GET request with the specified user's email address.
            HttpResponseMessage response = await Client.GetAsync($"drivers/{userId}");

            // Verify that the HTTP Get request was successful.
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.OK);

            // Return deserialized user object retrieved from the response message.
            return await GetDeserializedPayload<DriverDto>(response);
        }
    }
}