using AuthService.BOs;
using AuthService.Models;

namespace AuthService.TokenService
{
    public interface IJWTTokenService
    {
        LoginResponseBO GenerateToken(AuthCredential credential);
    }
}
