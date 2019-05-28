using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EzRide.Infrastructure.DTO;

namespace EzRide.Infrastructure.Services
{
    public interface IUserService : IService
    {
        Task<UserDto> GetAsync(Guid id);

        Task<UserDto> GetAsync(string username);

        Task<IEnumerable<UserDto>> BrowseAsync();
        
        Task RegisterAsync(Guid id, string email, string username, string password, string role);

        Task LoginAsync(string username, string password);
    }
}