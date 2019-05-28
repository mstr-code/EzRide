using System.Threading.Tasks;

using EzRide.Infrastructure.Commands.Users;
using EzRide.Infrastructure.DTO;
using EzRide.Infrastructure.Extensions;
using EzRide.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;

namespace EzRide.Infrastructure.Handlers.Users
{
    public class LoginHandler : ICommandHandler<Login>
    {
        private readonly IUserService userService;
        private readonly IJwtHandler jwtHandler;
        private readonly IMemoryCache memoryCache;

        public LoginHandler(IUserService userService,
            IJwtHandler jwtHandler, IMemoryCache memoryCache)
        {
            this.userService = userService;
            this.jwtHandler = jwtHandler;
            this.memoryCache = memoryCache;
        }

        public async Task HandleAsync(Login command)
        {
            await userService.LoginAsync(command.Username, command.Password);
            UserDto user = await userService.GetAsync(command.Username);
            JwtDto jwt = jwtHandler.CreateToken(user.Role, command.Username);
            memoryCache.SetJwt(command.TokenId, jwt);
        }
    }
}