using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EzRide.Core.Domain;
using EzRide.Core.Repositories;

namespace EzRide.Infrastructure.Repositories
{
    public class DriverRepositoryInMemory : IDriverRepository
    {
        private static ISet<Driver> drivers = new HashSet<Driver>();

        public async Task AddAsync(Driver driver)
        {
            drivers.Add(driver);
            await Task.CompletedTask;
        }

        public async Task<Driver> GetAsync(Guid userId) =>
            await Task.FromResult(drivers.SingleOrDefault(x => x.UserId == userId));

        public async Task<Driver> GetAsync(string username) =>
            await Task.FromResult(drivers.SingleOrDefault(x => x.Username == username));

        public async Task<IEnumerable<Driver>> BrowseAsync() =>
            await Task.FromResult(drivers);

        public async Task RemoveAsync(Guid id)
        {
            Driver driver = await GetAsync(id);
            drivers.Remove(driver);
        }

        public async Task UpdateAsync(Driver driver)
        {
            await Task.CompletedTask;
        }
    }
}