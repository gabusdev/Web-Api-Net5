using AutoMapper;
using Common.Request;
using Core.Entities.Enums;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Exceptions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services.Impl
{
    public class AuthManager : IAuthManager
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AuthManager(IConfiguration configuration, UserManager<User> userManager, IMapper mapper)
        {
            _configuration = configuration;
            _userManager = userManager;
            _mapper = mapper;
        }
        public async Task<(User user, string accessToken)> AuthenticateAsync(LoginDTO loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            
            if (user == null)
            {
   throw new UserNotFoundException("The User with such Mail doesn't exist");
            }
            var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!result)
            {
                throw new UnauthorizedException("The Password provided is wrong");
            }
            
            return (user, await CreateToken(user));
        }
c async Task<User> RegisterAsync(RegisterDTO registerDto)
        {
            var user = _mapper.Map<User>(registerDto);
            
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
   throw new InvalidFieldBadRequestException(result.Errors.First().Description, 400003);
            
            result = await _userManager.AddToRoleAsync(user, Enum.GetName(RoleEnum.User));
            if (!result.Succeeded)
   throw new InvalidFieldBadRequestException(result.Errors.First().Description, 400004);

            return user;
        }

        private async Task<string> CreateToken(User user)
        {
            var credentials = GetSigningCredentials();
            var claims = await GetClaims(user);
            
            return GetToken(credentials, claims);
        }
te SigningCredentials GetSigningCredentials()
        {
            var key = Environment.GetEnvironmentVariable("JwtKey")
                ?? _configuration.GetSection("Jwt").GetValue<string>("Key");
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
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
te string GetToken(SigningCredentials credentials, IEnumerable<Claim> claims, bool encrypt = false)
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

}
