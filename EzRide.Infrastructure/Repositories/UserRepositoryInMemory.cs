using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EzRide.Core.Domain;
using EzRide.Core.Repositories;

namespace EzRide.Infrastructure.Repositories
{
    public class UserRepositoryInMemory : IUserRepository
    {
        private static ISet<User> users = new HashSet<User>();
        
        public async Task AddAsync(User user)
        {
            users.Add(user);
            await Task.CompletedTask;
        }

        public async Task<User> GetAsync(Guid id) =>
            await Task.FromResult(users.SingleOrDefault(x => x.Id == id));

        public async Task<User> GetAsync(string username) =>
            await Task.FromResult(users.SingleOrDefault(x => x.Username == username));

        public async Task<IEnumerable<User>> BrowseAsync() =>
            await Task.FromResult(users);

        public async Task RemoveAsync(Guid id)
        {
            User user = await GetAsync(id);
            users.Remove(user);
        }

        public async Task UpdateAsync(User user)
        {
            await Task.CompletedTask;
        }
    }
}