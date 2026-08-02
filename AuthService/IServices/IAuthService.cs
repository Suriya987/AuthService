using AuthService.BOs;
namespace AuthService.Services;

public interface IAuthService
{
    Task SaveCredential(SaveCredentialBO request);
}