
using AuthService.DbContexts;

namespace AuthService.IFactory;
public interface IDbChatApplicationContextFactory
{
    Task<ChatApplicationDbContext> CreateDbContextAsync();
}