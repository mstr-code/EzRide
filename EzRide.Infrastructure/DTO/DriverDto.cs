using System;

using EzRide.Core.Domain;

namespace EzRide.Infrastructure.DTO
{
    public class DriverDto
    {
        // public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}