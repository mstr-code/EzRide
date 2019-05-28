using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EzRide.Infrastructure.Commands;
using EzRide.Infrastructure.Commands.Users;
using EzRide.Infrastructure.DTO;
using EzRide.Infrastructure.Services;
using EzRide.Infrastructure.Settings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EzRide.Api.Controllers
{
    public class UsersController : ApiControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService,
            ICommandDispatcher commandDispatcher)
            : base(commandDispatcher)
        {
            this.userService = userService;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetAsync(string username)
        {
            UserDto user = await userService.GetAsync(username);
            if (user == null)
                return NotFound();
            return Json(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            IEnumerable<UserDto> users = await userService.BrowseAsync();
            if (users == null)
                return NotFound();
            return Json(users);
        }
        
        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateUser command)
        {
            await CommandDispatcher.DispatchAsync(command);

            // The name of this controller is 'Users', so the path is 'users/'.
            return Created($"users/{command.Username}", null);
        }
    }
}