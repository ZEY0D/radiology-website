using RadiologyHIS.API.Models;

namespace RadiologyHIS.API.Data.Services
{
    public interface IAuthService
    {
        Task<bool> SignUpAsync(SignUpRequest request);
        Task<User?> LoginAsync(LoginRequest request);
    }
}
