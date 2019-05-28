using System.Threading.Tasks;

using EzRide.Core.Domain;
using EzRide.Infrastructure.Commands;
using EzRide.Infrastructure.Commands.Drivers;
using EzRide.Infrastructure.Services;
using static EzRide.Infrastructure.Commands.Drivers.CreateDriver;

namespace EzRide.Infrastructure.Handlers.Drivers
{
    public class CreateDriverHandler : ICommandHandler<CreateDriver>
    {
        private readonly IDriverService driverService;

        public CreateDriverHandler(IDriverService driverService)
        {
            this.driverService = driverService;
        }

        public async Task HandleAsync(CreateDriver command)
        {
            DriverVehicle driverVehicle = command.Vehicle;
            Vehicle vehicle = Vehicle.Create(driverVehicle.Brand, driverVehicle.Model,
                driverVehicle.Color, driverVehicle.SeatingCapacity);
            await driverService.RegisterAsync(command.UserId, vehicle);
        }
    }
}