using System;

using EzRide.Infrastructure.DTO;
using Microsoft.Extensions.Caching.Memory;

namespace EzRide.Infrastructure.Extensions
{
    public static class CacheExtensions
    {
        public static void SetJwt(
            this IMemoryCache memoryCache, Guid tokenId, JwtDto jwtDto) =>
            memoryCache.Set(GetJwtKey(tokenId), jwtDto, TimeSpan.FromSeconds(5));
        
        public static JwtDto GetJwt(this IMemoryCache memoryCache, Guid tokenId) =>
            memoryCache.Get<JwtDto>(GetJwtKey(tokenId));
        
        private static string GetJwtKey(Guid tokenId) => $"jwt-{tokenId}";
    }
}