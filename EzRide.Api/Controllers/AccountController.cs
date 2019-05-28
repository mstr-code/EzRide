using System.Threading.Tasks;

using EzRide.Infrastructure.Commands;
using EzRide.Infrastructure.Commands.Users;
using EzRide.Infrastructure.DTO;
using EzRide.Infrastructure.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EzRide.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        private readonly IJwtHandler jwtHandler;

        public AccountController(ICommandDispatcher commandDispatcher,
            IJwtHandler jwtHandler)
                : base(commandDispatcher)
        {
            this.jwtHandler = jwtHandler;
        }

        [HttpPut("{password}")]
        public async Task<IActionResult> PutAsync(ChangeUserPassword command)
        {
            await CommandDispatcher.DispatchAsync(command);

            // Http status code 204: NoContent
            // Request successfully processed by the server; no returning content.
            return NoContent();
        }
    }
}