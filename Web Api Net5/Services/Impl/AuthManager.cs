﻿using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Web_Api_Net5.Models;

namespace Web_Api_Net5.Services.Impl
{
    public class AuthManager : IAuthManager
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public AuthManager(IConfiguration configuration, UserManager<User> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<(User user, string accessToken, string refreshToken)> LoginAsync(LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            
            return (user != null && await _userManager.CheckPasswordAsync(user, loginDto.Password))
                 ? (user, await CreateToken(user), GetRefreshToken())
                 : (null, null, null);
        }
        
        private async Task<string> CreateToken(User user)
        {
            var credentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            
            return GetToken(credentials, claims);
        }
        private SigningCredentials GetSigningCredentials()
        {
            var key = Environment.GetEnvironmentVariable("JwtKey")
                ?? _configuration.GetSection("Jwt").GetValue<string>("Key");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.Sha256);
        }
        private async Task<List<Claim>> GetClaims(User user)
        {
            var issuer = _configuration.GetSection("Jwt").GetValue<string>("Issuer");
            var roles = await _userManager.GetRolesAsync(user);
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email, ClaimValueTypes.Email, issuer),
                new Claim(ClaimTypes.AuthenticationMethod, "bearer", ClaimValueTypes.String, issuer),
                new Claim(ClaimTypes.NameIdentifier, user.UserName, ClaimValueTypes.String, issuer),
                new Claim(ClaimTypes.UserData, user.Id, ClaimValueTypes.String, issuer)
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role, ClaimValueTypes.String, issuer));
            }
            
            return claims;
        }
        private string GetToken(SigningCredentials credentials, IEnumerable<Claim> claims, bool encrypt = false)
        {
            var issuer = _configuration.GetSection("Jwt")["Issuer"];
            
            var jwt = new JwtSecurityToken(
                issuer: issuer,
                audience: _configuration.GetSection("Jwt")["Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration.GetSection("Jwt")["ExpireTime"])),
                signingCredentials: credentials
            );
            
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        private string GetRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}