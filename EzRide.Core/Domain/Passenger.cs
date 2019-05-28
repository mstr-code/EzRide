using System;

namespace EzRide.Core.Domain
{
    public class Passenger
    {
        public Guid Id { get; protected set; }
        public Guid UserId { get; protected set; }
        public Stop PickUpStop { get; protected set; }
        public Stop DropOffStop { get; protected set; }

        protected Passenger() { }

        public Passenger(Stop pickUpStop, Stop dropOffStop)
        {
            Id = Guid.NewGuid();
            PickUpStop = pickUpStop;
            DropOffStop = dropOffStop;
        }
    }
}