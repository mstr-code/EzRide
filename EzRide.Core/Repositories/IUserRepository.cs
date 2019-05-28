using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EzRide.Core.Domain;

namespace EzRide.Core.Repositories
{
    public interface IUserRepository : IRepository
    {
        // Add a user to the database.
        Task AddAsync(User user);
        // Fetch a user with a given ID from the database.
        Task<User> GetAsync(Guid id);
        // Fetch a user with a given username from the database.
        Task<User> GetAsync(string username);
        // Browse all users.
        Task<IEnumerable<User>> BrowseAsync();
        // Remove a user with a given ID from the database.
        Task RemoveAsync(Guid id);
        // Update user's information.
        Task UpdateAsync(User user);
    }
}