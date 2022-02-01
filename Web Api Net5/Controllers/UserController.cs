using AutoMapper;
using Core.Models;
using CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web_Api_Net5.Models;
using Web_Api_Net5.Services;

namespace Web_Api_Net5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly IUnitOfWork _uow;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly IAuthManager _authManager;

        public UserController(UserManager<User> userManager,
            ILogger<UserController> logger,
            IMapper mapper,
            IAuthManager authManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
            _authManager = authManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            _logger.LogInformation($"Registration Attemp for {registerDTO.Email}");
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var result = await _authManager.RegisterAsync(registerDTO);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            
            return Created(nameof(Register), "Success");
        }
            
        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            _logger.LogInformation($"Login Attemp for {loginDTO.Email}");
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            var (user, token) = await _authManager.AuthenticateAsync(loginDTO);
            if (user is null)
                throw new UnauthorizedException();

            return Accepted(new { Token = token });
        }

    }
}
