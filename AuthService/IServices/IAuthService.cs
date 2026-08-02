using AuthService.BOs;
using Microsoft.AspNetCore.Identity.Data;
namespace AuthService.Services;

public interface IAuthService
{
    Task SaveCredential(SaveCredentialBO request);
    Task<LoginResponseBO> Login(LoginRequest request);
    Task<LoginResponseBO> RefreshToken(RefreshTokenRequestBO request);
}