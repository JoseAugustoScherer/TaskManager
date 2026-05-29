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

    [HttpGet]
    public async Task<IActionResult> GetUsersAsync(CancellationToken cancellationToken)
    {
        var result = await userService.GetAllUsersAsync(cancellationToken);
        
        return Ok(result.Value);
    }

    [HttpPatch("update/{userId:guid}")]
    public async Task<IActionResult> UpdateUserAsync(Guid userId, UpdateUserRequestDto request,
        CancellationToken cancellationToken)
    {
        var result = await userService.UpdateUserAsync(userId, request, cancellationToken);
        
        return result.IsFailure ? StatusCode(result.StatusCode, result) : Ok(result.Value);
    }
}