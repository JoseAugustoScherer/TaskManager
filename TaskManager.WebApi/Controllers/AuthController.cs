using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Dto.Request.User;
using TaskManager.Application.Interfaces.Services.Auth;

namespace TaskManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController (
    ILoginService loginService
    ) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginRequestDto requestDto,
        CancellationToken cancellationToken)
    {
        var result = await loginService.LoginAsync(requestDto, cancellationToken);

        return result.IsFailure ? StatusCode(result.StatusCode, result) : Ok(result);
    }
}