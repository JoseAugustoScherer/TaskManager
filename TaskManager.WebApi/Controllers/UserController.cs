using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Dto.Request.User;
using TaskManager.Application.Interfaces.Services.User;

namespace TaskManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController (
    IUserService userService
) :ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateUser(UserRequestDto request, CancellationToken cancellationToken)
    {
        var result = await userService.CreateUserAsync(request, cancellationToken);
        
        return result.IsFailure ? StatusCode(result.StatusCode, result) : Created($"/user/{result.Value.Id}", result.Value);
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var result = await userService.GetUserByIdAsync(userId, cancellationToken);
        
        return result.IsFailure ? StatusCode(result.StatusCode, result) : Ok(result.Value);
    }
}