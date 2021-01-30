using Microsoft.IdentityModel.Tokens;
using SuperChat.Core.ConfigModels;
using SuperChat.Datamodel.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SuperChat.Services.JWTFactory
{
    public class JwtFactory : IJwtFactory
    {
        private readonly AuthenticationSettings _authenticationSettings;
        public JwtFactory(AuthenticationSettings authenticationSettings)
        {
            _authenticationSettings = authenticationSettings;
        }
        public string GenerateToken(AppUser user)
        {
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var aKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authenticationSettings.Secret));
            var claims = new[] { 
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };
            var credentials = new SigningCredentials(aKey, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_authenticationSettings.Issuer, _authenticationSettings.Audience, claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: credentials);
            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}
