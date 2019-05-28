using EzRide.Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace EzRide.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public abstract class ApiControllerBase : Controller
    {
        protected ICommandDispatcher CommandDispatcher { get; }

        protected ApiControllerBase(ICommandDispatcher commandDispatcher)
        {
            CommandDispatcher = commandDispatcher;
        }
    }
}