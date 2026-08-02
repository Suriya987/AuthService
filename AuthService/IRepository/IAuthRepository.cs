using AuthService.Models;

namespace AuthService.Repository;

public interface IAuthRepository
{
    Task SaveCredential(AuthCredential credential);
    Task<AuthCredential?> GetCredentialByEmail(string email);
    Task SaveRefreshToken(RefreshToken refreshToken);
}