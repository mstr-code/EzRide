using System;
using System.Collections.Generic;

namespace EzRide.Core.Domain
{
    public class Driver
    {
        // public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public string Username { get; set; }
        public Vehicle Vehicle { get; protected set; }
        public IEnumerable<Route> Routes { get; protected set; }

        public DateTime UpdatedAt { get; protected set; }

        protected Driver() { }

        public Driver(User user, Vehicle vehicle)
        {
            // Id = Guid.NewGuid();
            UserId = user.Id;
            Username = user.Username;
            Vehicle = vehicle;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateVehicle(string brand, string model, string color, int seatingCapacity)
        {
            Vehicle.Create(brand, model, color, seatingCapacity);
            UpdatedAt = DateTime.UtcNow;
        }
    }
}