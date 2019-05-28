using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EzRide.Core.Domain;
using Microsoft.Extensions.Logging;

namespace EzRide.Infrastructure.Services
{
    public class DataInitializer : IDataInitializer
    {
        private readonly IUserService userService;
        private readonly IDriverService driverService;
        private readonly ILogger<DataInitializer> logger;

        public DataInitializer(IUserService userService, IDriverService driverService,
            ILogger<DataInitializer> logger)
        {
            this.userService = userService;
            this.driverService = driverService;
            this.logger = logger;
        }

        public async Task SeedAsync()
        {
            logger.LogTrace("Initializing data...");
            List<Task> tasks = new List<Task>();
            for (int i = 1; i <= 10; i++)
            {
                Guid userId = Guid.NewGuid();
                string username = $"user{i}";
                tasks.Add(userService.RegisterAsync(userId, $"{username}@domain.com", username, "secret", "user"));
                logger.LogTrace($"Created a new user: '{username}'.");
                if (i == 1)
                {
                    tasks.Add(driverService.RegisterAsync(userId, Vehicle.Create("Audi", "A1", "White", 4)));
                    logger.LogTrace($"User '{username}' registered as a driver.");
                }   
            }
            for (int i = 1; i <= 3; i++)
            {
                Guid userId = Guid.NewGuid();
                string username = $"admin{i}";
                tasks.Add(userService.RegisterAsync(userId, $"{username}@domain.com", username, "secret", "admin"));
                logger.LogTrace($"Created a new admin: '{username}'.");
            }
            await Task.WhenAll(tasks);
            logger.LogTrace("Data was initialized");
        }
    }
}