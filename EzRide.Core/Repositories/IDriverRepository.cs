using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EzRide.Core.Domain;

namespace EzRide.Core.Repositories
{
    public interface IDriverRepository : IRepository
    {
        // Add a driver to the database.
        Task AddAsync(Driver driver);
        // Fetch a driver with a given user ID from the database.
        Task<Driver> GetAsync(Guid userId);
        // Fetch a driver with a given username from the database.
        Task<Driver> GetAsync(string username);
        // Browse all drivers.
        Task<IEnumerable<Driver>> BrowseAsync();
        // Remove a driver with a given user ID from the database.
        Task RemoveAsync(Guid id);
        // Update user's information.
        Task UpdateAsync(Driver driver);
    }
}