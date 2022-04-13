using Common.Request;
using Core.Models;
using System.Threading.Tasks;


namespace Services
{
    public interface IAuthManager
    {
        Task<(User user, string accessToken)> AuthenticateAsync(LoginDTO loginDto);

        Task<User> RegisterAsync(RegisterDTO loginDto);
    }
}
