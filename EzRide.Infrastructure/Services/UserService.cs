using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;
using EzRide.Core.Domain;
using EzRide.Core.Repositories;
using EzRide.Infrastructure.DTO;

namespace EzRide.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IEncrypter encrypter;
        private readonly IMapper mapper;
        public UserService(
            IUserRepository userRepository, IEncrypter encrypter, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.encrypter = encrypter;
            this.mapper = mapper;
        }

        public async Task<UserDto> GetAsync(Guid id)
        {
            User user = await userRepository.GetAsync(id);
            return mapper.Map<User, UserDto>(user);
        }

        public async Task<UserDto> GetAsync(string username)
        {
            User user = await userRepository.GetAsync(username);
            return mapper.Map<User, UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> BrowseAsync()
        {
            IEnumerable<User> users = await userRepository.BrowseAsync();
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        public async Task RegisterAsync(Guid id, string email, string username, string password, string role)
        {
            User user = await userRepository.GetAsync(username);
            if (user != null)
                throw new Exception($"User with username: '{username}' already exists.");

            string salt = encrypter.GetSalt();
            string hash = encrypter.GetHash(password, salt);
            user = new User(id, email, username, hash, salt, role);
            await userRepository.AddAsync(user);
        }

        public async Task LoginAsync(string username, string password)
        {
            User user = await userRepository.GetAsync(username);
            if (user == null)
                throw new Exception("Invalid login credentials.");
            
            string hash = encrypter.GetHash(password, user.Salt);

            if (user.Password == hash)
                return;
            else
                throw new Exception("Invalid login credentials.");
        }
    }
}