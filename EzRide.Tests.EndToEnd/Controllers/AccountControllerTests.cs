using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using EzRide.Infrastructure.Commands.Users;
using FluentAssertions;
using Xunit;

namespace EzRide.Tests.EndToEnd.Controllers
{
    public class AccountControllerTests : ControllerTestsBase
    {
        public AccountControllerTests() : base() { }

        [Fact]
        public async Task successful_change_of_user_password()
        {
            // Arrange
            ChangeUserPassword command = new ChangeUserPassword()
            {
                CurrentPassword = "secret",
                NewPassword = "top secret"
            };

            // Act
            HttpContent payload = GetSerializedPayload(command);
            HttpResponseMessage response =
                await Client.PutAsync("account/password", payload);

            // Assert
            //response.EnsureSuccessStatusCode();
            response.StatusCode.Should().BeEquivalentTo(HttpStatusCode.NoContent);
        }
    }
}