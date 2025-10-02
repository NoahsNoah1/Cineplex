using CinePlex.Dtos;
using CinePlex.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CinePlex.Services
{
    public class AuthService: IAuthService
    {
        private readonly IuserRepository _repo;
        private readonly IConfiguration _configuration;
        public AuthService(IuserRepository repo, IConfiguration configuration)
        {
            _repo = repo;
            _configuration = configuration;
        }
        public bool UserNameExists(string username)
        {    
            var user=_repo.GetByusername(username);
            if (user != null)
                return true;
            return false;
        }
        public bool AuthenticatedUser(LoginDto loginDto)
        {
            var user = _repo.GetByusername(loginDto.Username);
            if (user.Data.UserName == loginDto.Username && user.Data.UserType == loginDto.UserType && user.Data.Password == loginDto.Password)
                return true;
            return false;
        }
        public string GenerateJwtToken(LoginDto loginDto)
        {
            var key= Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecurityKey"]);
            var jwtToken = new JwtSecurityToken
                (
                    issuer: "https://localhost:7064",
                    audience: "https://localhost:7064",
                    claims: new[]
                    {
                        new Claim(ClaimTypes.Role, loginDto.Username)
                    },
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                );
            var tokenHandler=new JwtSecurityTokenHandler();
            var token=tokenHandler.WriteToken(jwtToken);
            return token;
        }
    }
}
