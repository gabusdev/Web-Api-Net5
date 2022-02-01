using AutoMapper;
using Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web_Api_Net5.Models;
using Web_Api_Net5.Repository;

namespace Web_Api_Net5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        //private readonly IUnitOfWork _uow;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager,
            ILogger<AccountController> logger,
            IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            _logger.LogInformation($"Registration Attemp for {registerDTO.Email}");
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var user = _mapper.Map<User>(registerDTO);
                var result = await _userManager.CreateAsync(user, registerDTO.Password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(error.Code, error.Description);
                    }
                    return BadRequest(ModelState);
                }

                await _userManager.AddToRolesAsync(user, new List<string> { "User" } );
                return Created("",true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something went wrong at {nameof(Register)}");
                return Problem($"Something went wrong at {nameof(Register)}", statusCode: StatusCodes.Status500InternalServerError);
            }
        }
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        //{
        //    _logger.LogInformation($"Login Attemp for {loginDTO.Email}");
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    try
        //    {
        //        var result = await _sigInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, false, false);
        //        if (!result.Succeeded)
        //            return Unauthorized("Wrong credentials");
        //        return Accepted();
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Something went wrong at {nameof(Login)}");
        //        return Problem($"Something went wrong at {nameof(Login)}", statusCode: StatusCodes.Status500InternalServerError);
        //    }
        //}

    }
}
