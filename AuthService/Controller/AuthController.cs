using AuthService.BOs;
using AuthService.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace AuthService.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;


    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    [HttpPost("save-credential")]
    public async Task<IActionResult> SaveCredential(SaveCredentialBO request)
    {
        await _authService.SaveCredential(request);

        return Ok(new
        {
            Message = "Credential saved successfully"
        });
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseBO>> Login(
    LoginRequest request)
    {
        var result = await _authService.Login(request);

        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<ActionResult<LoginResponseBO>> RefreshToken(RefreshTokenRequestBO request)
    {
        var response = await _authService.RefreshToken(request);
        return Ok(response);
    }
}