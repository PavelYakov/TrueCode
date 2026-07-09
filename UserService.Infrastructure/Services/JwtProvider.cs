using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserService.Application.Common;
using UserService.Application.Interfaces.Services;
using UserService.Domain.Entites;

namespace UserService.Infrastructure.Services
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtSettings _settings;

        public JwtProvider(IOptions<JwtSettings> options)
        {
            _settings = options.Value;
        }


        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(
                ClaimTypes.NameIdentifier,
                user.Id.ToString()),

                new Claim(
                ClaimTypes.Name,
                user.Name)
            };


            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_settings.Key));


            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);


            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(
                    _settings.ExpireMinutes),
                signingCredentials: credentials);


            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
