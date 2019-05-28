using System;

namespace EzRide.Infrastructure.Commands.Drivers
{
    public class CreateDriver : ICommand
    {
        // public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public DriverVehicle Vehicle { get; set; }
        
        public class DriverVehicle
        {
            public string Brand { get; set; }
            public string Model { get; set; }
            public string Color { get; set; }
            public int SeatingCapacity { get; set; }
        }
    }
}