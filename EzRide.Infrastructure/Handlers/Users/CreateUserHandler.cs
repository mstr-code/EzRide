using System.Threading.Tasks;

using EzRide.Infrastructure.Commands;
using EzRide.Infrastructure.Commands.Users;
using EzRide.Infrastructure.Services;

namespace EzRide.Infrastructure.Handlers.Users
{
    public class CreateUserHandler : ICommandHandler<CreateUser>
    {
        private readonly IUserService userService;

        public CreateUserHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task HandleAsync(CreateUser command)
        {
            await userService.RegisterAsync(command.Id, command.Email, command.Username, command.Password, command.Role);
        }
    }
}