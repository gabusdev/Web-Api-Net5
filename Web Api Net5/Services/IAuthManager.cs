using Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Web_Api_Net5.Models;

namespace Web_Api_Net5.Services
{
    public interface IAuthManager
    {
        Task<(User user, string accessToken)> AuthenticateAsync(LoginDTO loginDto);

        Task<IdentityResult> RegisterAsync(RegisterDTO loginDto);
    }
}
