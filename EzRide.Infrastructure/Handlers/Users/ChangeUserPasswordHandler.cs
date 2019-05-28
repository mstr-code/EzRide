using System.Threading.Tasks;

using EzRide.Infrastructure.Commands;
using EzRide.Infrastructure.Commands.Users;
using EzRide.Infrastructure.Services;

namespace EzRide.Infrastructure.Handlers.Users
{
    public class ChangeUserPasswordHandler : ICommandHandler<ChangeUserPassword>
    {
        private readonly IUserService userService;

        public ChangeUserPasswordHandler(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task HandleAsync(ChangeUserPassword command)
        {
            await Task.CompletedTask;
        }
    }
}