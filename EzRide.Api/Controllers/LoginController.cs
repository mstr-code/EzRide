using System;
using System.Threading.Tasks;

using EzRide.Infrastructure.Commands;
using EzRide.Infrastructure.Commands.Users;
using EzRide.Infrastructure.DTO;
using EzRide.Infrastructure.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EzRide.Api.Controllers
{
    public class LoginController : ApiControllerBase
    {
        private readonly IMemoryCache memoryCache;
        
        public LoginController(ICommandDispatcher commandDispatcher,
            IMemoryCache memoryCache) : base(commandDispatcher)
        {
            this.memoryCache = memoryCache;
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Login command)
        {
            command.TokenId = Guid.NewGuid();
            await CommandDispatcher.DispatchAsync(command);
            JwtDto jwt = memoryCache.GetJwt(command.TokenId);

            return Json(jwt);
        }
    }
}