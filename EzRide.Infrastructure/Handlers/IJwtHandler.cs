using EzRide.Infrastructure.DTO;

namespace EzRide.Infrastructure.Handlers
{
    public interface IJwtHandler
    {
        JwtDto CreateToken(string role, string username);
    }
}