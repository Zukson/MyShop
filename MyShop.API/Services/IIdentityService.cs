using MyShop.API.Domain;
using System.Threading.Tasks;

namespace MyShop.API.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
    }
}