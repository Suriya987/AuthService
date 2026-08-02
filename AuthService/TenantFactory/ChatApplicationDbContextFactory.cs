using AuthService.IFactory;
using AuthService.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace AuthService.TenantFactory
{
    public class ChatApplicationDbContextFactory : IDbChatApplicationContextFactory
    {
        private readonly IDbContextFactory<ChatApplicationDbContext> _factory;

        public ChatApplicationDbContextFactory(IDbContextFactory<ChatApplicationDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<ChatApplicationDbContext> CreateDbContextAsync()
        {
            return await _factory.CreateDbContextAsync();
        }
    }

}
