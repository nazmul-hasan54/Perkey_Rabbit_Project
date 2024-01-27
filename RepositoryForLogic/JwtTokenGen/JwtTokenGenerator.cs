using Domain.Models;
using InterfacesToContract;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryForLogic.JwtTokenGen
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IDateTimeProvider _dateTimePrivder;
        private readonly JwtSettings _jwtSettings;
        public JwtTokenGenerator(IDateTimeProvider dateTimePrivder, IOptions<JwtSettings> option) 
        { 
            _jwtSettings = option.Value;
            _dateTimePrivder = dateTimePrivder;
        }
        public string GenerateToken(Users user)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub, user.UsersId.ToString()),
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            claims.Add(new Claim(ClaimTypes.Role, user.Role));

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: _dateTimePrivder.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: signingCredentials
                );
            return new JwtSecurityTokenHandler().WriteToken( securityToken );
        }
    }
}
