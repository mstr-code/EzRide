using System;
using System.Text.RegularExpressions;

namespace EzRide.Core.Domain
{
    public class Stop
    {
        private string address;
        private double longitude;
        private double latitude;
        
        private static readonly Regex RegexName = new Regex("[A-Za-z0-9]");

        public DateTime UpdatedAt { get; protected set; }

        public string Address
        {
            get => address;
            set
            {
                if (!RegexName.IsMatch(value))
                    throw new System.Exception("Please provide a valid Address.");
                if (address == value)
                    return;
                address = value;
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public double Longitude
        {
            get => longitude;
            set
            {
                if (double.IsNaN(value))
                    throw new System.Exception("Please provide a valid Longitude.");
                if (longitude == value)
                    return;
                longitude = value;
                UpdatedAt = DateTime.UtcNow;
            }
        }

        public double Latitude
        {
            get => latitude;
            set
            {
                if (double.IsNaN(value))
                    throw new System.Exception("Please provide a valid Latitude.");
                if (latitude == value)
                    return;
                latitude = value;
                UpdatedAt = DateTime.UtcNow;
            }
        }

        

        protected Stop() { }

        private Stop(string address, double longitude, double latitude)
        {
            Address = address;
            Longitude = longitude;
            Latitude = latitude;
        }

        public static Stop Create(string address, double longitude, double latitude)
            => new Stop(address, longitude, latitude);
    }
}