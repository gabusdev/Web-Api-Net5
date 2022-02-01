using Common.Request;
using Core.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;


namespace Services
{
    public interface IAuthManager
    {
        Task<(User user, string accessToken)> AuthenticateAsync(LoginDTO loginDto);

        Task<IdentityResult> RegisterAsync(RegisterDTO loginDto);
    }
}
