using Microsoft.AspNetCore.Mvc;
using Server.Services;
using SharedLibrary.Requests;

namespace Server.Controllers;
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public IActionResult Register(AuthRequest request)
    {
        var (success, content) = _authService.Register(request.Username, request.Password);
        if (!success)
        {
            return BadRequest(content);
        }

        return Login(request);
    }
    
    [HttpPost("login")]
    public IActionResult Login(AuthRequest request)
    {
        var (success, content) = _authService.Login(request.Username, request.Password);
        if (!success)
        {
            return BadRequest(content);
        }

        return Ok(new AuthResponse()
        {
            Token = content
        });
    }
}