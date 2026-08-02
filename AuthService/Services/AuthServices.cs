using AuthService.BOs;
using AuthService.Models;
using AuthService.Repository;
using AuthService.TokenService;
using Microsoft.AspNetCore.Identity.Data;
using System.Security.Cryptography;
using System.Text;

namespace AuthService.Services;

public class AuthServices : IAuthService
{
    private readonly IAuthRepository _repository;
    private readonly IJWTTokenService _jwtTokenService;


    public AuthServices(IAuthRepository repository, IJWTTokenService jwtTokenService)
    {
        _repository = repository;
        _jwtTokenService = jwtTokenService;
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
            UpdatedAt = DateTime.UtcNow,
            Email= request.Email
        };


        await _repository.SaveCredential(credential);
    }

    public async Task<LoginResponseBO> Login(LoginRequest request)
    {
        var credential =
            await _repository
                .GetCredentialByEmail(request.Email);

        if (credential == null)
            throw new Exception("Invalid Email");

        bool isValid =
            BCrypt.Net.BCrypt.Verify(
                request.Password,
                credential.PasswordHash);


        if (!isValid)
            throw new Exception("Invalid Password");

        // Generate Access Token
        var response =
            _jwtTokenService.GenerateToken(credential);

        // Generate Refresh Token
        var refreshToken = GenerateRefreshToken();

        // Hash Refresh Token
        var tokenHash =ComputeHash(refreshToken);

        var RefToken = new RefreshToken
        {
            UserId = credential.UserId ?? 0,

            TokenHash = tokenHash,

            CreatedAt = DateTime.UtcNow,

            ExpiresAt = DateTime.UtcNow.AddDays(30),

            RevokedAt = null
        };

        // Save Refresh Token
        await _repository.SaveRefreshToken(RefToken);
           
        // Return plain refresh token
        response.RefreshToken = refreshToken;

        return response;
    }

    private string GenerateRefreshToken()
    {
        return Convert.ToBase64String(
            RandomNumberGenerator.GetBytes(64));
    }

    private string ComputeHash(string token)
    {
        using var sha = SHA256.Create();

        var hash = sha.ComputeHash(
            Encoding.UTF8.GetBytes(token));

        return Convert.ToHexString(hash);
    }
}