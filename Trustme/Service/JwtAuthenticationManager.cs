﻿using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Trustme.IServices;
using Trustme.Models;

namespace Trustme.Service
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly IConfiguration _Configuration;
        public JwtAuthenticationManager(IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public string GenerateTokenForUser(User user)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var privateKey = _Configuration.GetValue<string>("PrivateKey");
            var tokenKey = Encoding.ASCII.GetBytes(privateKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}
