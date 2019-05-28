using System;

namespace EzRide.Core.Domain
{
    public class Vehicle
    {
        private int seatingCapacity;
        public string Brand { get; protected set; }
        public string Model { get; protected set; }
        public string Color { get; protected set; }
        public int SeatingCapacity
        {
            get => seatingCapacity;
            set
            {
                if (value < 2)
                    throw new Exception("Seating capacity must be greater than 1");
                if (value > 9)
                    throw new Exception("Seating capacity cannot be greater than 9");
                if (value == seatingCapacity)
                    return;
                seatingCapacity = value;
            }
        }

        protected Vehicle() { }

        private Vehicle(string brand, string model, string color, int seatingCapacity)
        {
            Brand = brand;
            Model = model;
            Color = color;
            SeatingCapacity = seatingCapacity;
        }

        public static Vehicle Create(string brand, string model, string color, int seatingCapacity)
            => new Vehicle(brand, model, color, seatingCapacity);
    }
}