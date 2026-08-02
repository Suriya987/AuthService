using AuthService.Models;

namespace AuthService.Repository;

public interface IAuthRepository
{
    Task SaveCredential(AuthCredential credential);
    Task<AuthCredential?> GetCredentialByEmail(string email);
    Task SaveRefreshToken(RefreshToken refreshToken);
    Task<RefreshToken?> GetRefreshTokenByHash(string tokenHash);

    Task<AuthCredential?> GetCredentialByUserId(long userId);

    Task RevokeRefreshToken(long refreshTokenId);

    //Task SaveRefreshToken(RefreshToken refreshToken);
}