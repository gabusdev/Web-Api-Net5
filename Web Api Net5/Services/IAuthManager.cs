using Core.Models;
using System.Threading.Tasks;
using Web_Api_Net5.Models;

namespace Web_Api_Net5.Services
{
    public interface IAuthManager
    {
        Task<(User user, string accessToken, string refreshToken)> LoginAsync(LoginDTO loginDto);
    }
}
