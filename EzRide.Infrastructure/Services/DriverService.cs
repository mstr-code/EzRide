using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;
using EzRide.Core.Domain;
using EzRide.Core.Repositories;
using EzRide.Infrastructure.DTO;

namespace EzRide.Infrastructure.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository driverRepository;
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public DriverService(IDriverRepository driverRepository,
            IUserRepository userRepository, IMapper mapper)
        {
            this.driverRepository = driverRepository;
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<DriverDto> GetAsync(Guid userId)
        {
            Driver driver = await driverRepository.GetAsync(userId);
            return mapper.Map<Driver, DriverDto>(driver);
        }

        public async Task<DriverDto> GetAsync(string username)
        {
            Driver driver = await driverRepository.GetAsync(username);
            return mapper.Map<Driver, DriverDto>(driver);
        }

        public async Task<IEnumerable<DriverDto>> BrowseAsync()
        {
            IEnumerable<Driver> drivers = await driverRepository.BrowseAsync();
            return mapper.Map<IEnumerable<Driver>, IEnumerable<DriverDto>>(drivers);
        }

        public async Task RegisterAsync(Guid userId, Vehicle vehicle)
        {
            User user = await userRepository.GetAsync(userId);
            if (user == null)
                throw new Exception($"User with ID: '{userId}' was not found.");

            Driver driver = await driverRepository.GetAsync(userId);
            if (driver != null)
                throw new Exception($"Driver with user ID: '{userId}' already exists.");
            
            driver = new Driver(user, vehicle);
            await driverRepository.AddAsync(driver);
        }

        public async Task UpdateVehicleAsync(Guid userId, string brand, string model, string color, int seatingCapacity)
        {
            Driver driver = await driverRepository.GetAsync(userId);
            if (driver == null)
                throw new Exception($"Driver with user ID: '{userId}' was not found.");
            
            driver.UpdateVehicle(brand, model, color, seatingCapacity);
            await driverRepository.UpdateAsync(driver);
        }
    }
}