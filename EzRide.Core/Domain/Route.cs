using System;
using System.Collections.Generic;

namespace EzRide.Core.Domain
{
    public class Route
    {
        // The route's list of stops (backing field).
        private ISet<Stop> stops = new HashSet<Stop>();
        // The route's identificatior.
        public Guid Id { get; protected set; }
        // The route's list of stops.
        public IEnumerable<Stop> Stops => stops;

        protected Route() { }

        public void AddStop(Stop stop) =>
            stops.Add(Stop.Create(stop.Address, stop.Longitude, stop.Latitude));
        
        public void RemoveStop(Stop stop) => stops.Remove(stop);

        public bool StopExists(Stop stop)
        {
            return stops.Contains(stop) ? true : false;
        }
    }
}