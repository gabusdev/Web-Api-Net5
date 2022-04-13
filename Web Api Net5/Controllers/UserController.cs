using AutoMapper;
using BasicResponses;
using Common.Request;
using Common.Response;
using Core.Entities.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services;
using System.Threading.Tasks;

namespace Web_Api_Net5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly IUnitOfWork _uow;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;
        private readonly IValidator<LoginDTO> _loginValidator;
        private readonly IValidator<RegisterDTO> _registerValidator;

        public UserController(
            ILogger<UserController> logger,
            IMapper mapper,
            IAuthManager authManager,
            IValidator<LoginDTO> loginValidator,
            IValidator<RegisterDTO> registerValidator)
        {
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            _logger.LogInformation($"Registration Attemp for {registerDTO.Email}");
            var result = _registerValidator.Validate(registerDTO);
            if (!result.IsValid)
                return BadRequest(new ApiBadRequestResponse(result));

            var user = await _authManager.RegisterAsync(registerDTO);
            var userDto = _mapper.Map<UserDTO>(user);
            return CreatedAtAction("", userDto);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            _logger.LogInformation($"Login Attemp for {loginDTO.Email}");
            var result = _loginValidator.Validate(loginDTO);
            if (!result.IsValid)
                return BadRequest(new ApiBadRequestResponse(result));

            var (_, token) = await _authManager.AuthenticateAsync(loginDTO);

            return Accepted(new { Token = token });
        }

        [HttpGet("test")]
        public string Test()
        {
            return nameof(RoleEnum.Admin);
        }

    }
}
