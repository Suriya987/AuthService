using AuthService.BOs;
using AuthService.Services;
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
}