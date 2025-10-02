using CinePlex.Dtos;

namespace CinePlex.Services
{
    public interface IAuthService
    {
        bool UserNameExists(string username);
        bool AuthenticatedUser(LoginDto loginDto);
        string GenerateJwtToken(LoginDto loginDto);
    }
}
