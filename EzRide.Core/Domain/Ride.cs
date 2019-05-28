using System;
using System.Collections.Generic;

namespace EzRide.Core.Domain
{
    public class Ride
    {
        // The ride's identificator.
        public Guid Id { get; protected set; }
        // The ride's route.
        public Route Route { get; protected set; }
        // The ride's driver.
        public Driver Driver { get; protected set; }
        // The ride's list of passengers.
        public IEnumerable<Passenger> Passengers { get; protected set; }
    }
}