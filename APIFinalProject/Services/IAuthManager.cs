using APIFinalProject.DTO;

namespace APIFinalProject.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginDTO loginDTO);
        Task<string> CreateToken();
    }
}
