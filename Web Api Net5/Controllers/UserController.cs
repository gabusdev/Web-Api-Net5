using AutoMapper;
using BasicResponses;
using Common.Request;
using Common.Response;
using Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Services;
using System.Linq;
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

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
            _logger.LogInformation($"Registration Attemp for {registerDTO.Email}");
            if (!ModelState.IsValid)
                return BadRequest(new ApiBadRequestResponse(ModelState));

            var user = await _authManager.RegisterAsync(registerDTO);
            var userDto = _mapper.Map<UserDTO>(user);
            return CreatedAtAction("", userDto);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            _logger.LogInformation($"Login Attemp for {loginDTO.Email}");
            if (!ModelState.IsValid)
                return BadRequest(new ApiBadRequestResponse(ModelState));

            var (_, token) = await _authManager.AuthenticateAsync(loginDTO);

            return Accepted(new { Token = token });
        }

    }
}
