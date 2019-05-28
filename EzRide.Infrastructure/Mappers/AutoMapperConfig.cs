using System;

using AutoMapper;
using EzRide.Core.Domain;
using EzRide.Infrastructure.DTO;

namespace EzRide.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize() =>
            new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
                cfg.CreateMap<Driver, DriverDto>();
            })
            .CreateMapper();
    }
}