using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using EzRide.Infrastructure.DTO;
using EzRide.Infrastructure.Extensions;
using EzRide.Infrastructure.Settings;
using Microsoft.IdentityModel.Tokens;

namespace EzRide.Infrastructure.Handlers
{
    public class JwtHandler : IJwtHandler
    {
        private readonly JwtSettings settings;

        public JwtHandler(JwtSettings settings) =>
            this.settings = settings;

        public JwtDto CreateToken(string role, string username)
        {
            DateTime time = DateTime.UtcNow;

            Claim[] claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(ClaimTypes.Role, role),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat,
                    time.ToUnixEpochTime().ToString(), ClaimValueTypes.Integer64)
            };

            DateTime expirationTime = time.AddMilliseconds(settings.ExpirationTime);
            // DateTime expirationTime = time.AddMilliseconds(300000);

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.Key)),
                // new SymmetricSecurityKey(Encoding.UTF8.GetBytes("Top_secret_key_123!")),
                SecurityAlgorithms.HmacSha256);
            
            var jwt = new JwtSecurityToken(
                issuer: settings.Issuer,
                // issuer: "http://localhost:5000",
                claims: claims,
                notBefore: time,
                expires: expirationTime,
                signingCredentials: signingCredentials
            );

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtDto
            {
                Token = token,
                Expires = expirationTime.ToUnixEpochTime()
            };
        }
    }
}