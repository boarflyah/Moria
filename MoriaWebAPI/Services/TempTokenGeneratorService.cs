﻿using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MoriaWebAPI.Services.Interfaces;

namespace MoriaWebAPI.Services;

/// <summary>
/// Generating jwt tokens for testing purposes
/// <para>Do not use this service in production</para>
/// </summary>
public class TempTokenGeneratorService : ITokenGeneratorService
{
    readonly string _ip;

    public TempTokenGeneratorService(string ip)
    {
        _ip = ip;
    }

    public string GenerateJwtToken(string userId)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("SuperSecretKey123456789101112131415");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] { new Claim("id", userId) }),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = _ip,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
