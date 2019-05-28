using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EzRide.Api.Controllers;
using EzRide.Infrastructure.Commands;
using EzRide.Infrastructure.Commands.Drivers;
using EzRide.Infrastructure.DTO;
using EzRide.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace EzRide.Core.Controllers
{
    public class DriversController : ApiControllerBase
    {
        private readonly IDriverService driverService;

        public DriversController(IDriverService driverService,
            ICommandDispatcher commandDispatcher)
            : base(commandDispatcher)
        {
            this.driverService = driverService;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetAsync(string username)
        {
            DriverDto driver = await driverService.GetAsync(username);
            if (driver == null)
                return NotFound();
            return Json(driver);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            IEnumerable<DriverDto> drivers = await driverService.BrowseAsync();
            if (drivers == null)
                return NotFound();
            return Json(drivers);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(CreateDriver command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Created($"drivers/{command.UserId}", null);
        }
    }
}