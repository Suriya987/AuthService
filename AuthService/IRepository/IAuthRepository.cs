using AuthService.Models;

namespace AuthService.Repository;

public interface IAuthRepository
{
    Task SaveCredential(AuthCredential credential);
}