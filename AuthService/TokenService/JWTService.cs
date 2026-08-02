using AuthService.BOs;
using AuthService.Models;
using AuthService.TokenService;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AuthService.TokenService;
public class JwtTokenService : IJWTTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public LoginResponseBO GenerateToken(AuthCredential credential)
    {
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!);

        var expiry = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryInMinutes"]));

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub,credential.UserId.ToString()),

            new Claim(JwtRegisteredClaimNames.Email,credential.Email),

            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(

            issuer: _configuration["Jwt:Issuer"],

            audience: _configuration["Jwt:Audience"],

            claims: claims,

            expires: expiry,

            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256)
        );

        return new LoginResponseBO
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),

            Expiry = expiry
        };
    }
}