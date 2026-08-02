using AuthService.DbContexts;
using AuthService.IFactory;
using AuthService.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Repository;

public class AuthRepository : IAuthRepository
{
    private readonly IDbChatApplicationContextFactory _contextFactory;


    public AuthRepository(IDbChatApplicationContextFactory context)
    {
        _contextFactory = context;
    }


    public async Task SaveCredential(AuthCredential credential)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();

        await context.AuthCredentials.AddAsync(credential);

        await context.SaveChangesAsync();
    }

    public async Task<AuthCredential?> GetCredentialByEmail(string email)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();

        return await context.AuthCredentials
            .FirstOrDefaultAsync(x => x.Email == email);
    }

    public async Task SaveRefreshToken(RefreshToken refreshToken)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();

        await context.RefreshTokens.AddAsync(refreshToken);

        await context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetRefreshTokenByHash(string tokenHash)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();

        return await context.RefreshTokens.FirstOrDefaultAsync(x =>x.TokenHash == tokenHash);
    }

    public async Task<AuthCredential?> GetCredentialByUserId(long userId)
    {

        await using var context = await _contextFactory.CreateDbContextAsync();

        return await context.AuthCredentials.FirstOrDefaultAsync(x => x.UserId == userId);
    }

    public async Task RevokeRefreshToken(long refreshTokenId)
    {
        await using var context = await _contextFactory.CreateDbContextAsync();

        var token = await context.RefreshTokens.FirstAsync(x => x.RefreshTokenId == refreshTokenId);

        token.RevokedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
    }


}