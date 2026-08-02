using AuthService.DbContexts;
using AuthService.IFactory;
using AuthService.Models;

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
}