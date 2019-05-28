using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using EzRide.Core.Domain;
using EzRide.Infrastructure.DTO;
using static EzRide.Infrastructure.Commands.Drivers.CreateDriver;

namespace EzRide.Infrastructure.Services
{
    public interface IDriverService : IService
    {
        Task<DriverDto> GetAsync(Guid userId);

        Task<DriverDto> GetAsync(string username);

        Task<IEnumerable<DriverDto>> BrowseAsync();

        Task RegisterAsync(Guid userId, Vehicle vehicle);

        Task UpdateVehicleAsync(Guid userId, string brand, string model, string color, int seatingCapacity);
    }
}