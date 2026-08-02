using AuthService.BOs;
using AuthService.Models;
using AuthService.Repository;

namespace AuthService.Services;

public class AuthServices : IAuthService
{
    private readonly IAuthRepository _repository;


    public AuthServices(IAuthRepository repository)
    {
        _repository = repository;
    }


    public async Task SaveCredential(SaveCredentialBO request)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

        var credential = new AuthCredential
        {
            UserId = request.UserId,
            PasswordHash = passwordHash,
            PasswordChangedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };


        await _repository.SaveCredential(credential);
    }
}