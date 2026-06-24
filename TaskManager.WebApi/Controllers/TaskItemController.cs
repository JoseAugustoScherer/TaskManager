using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Dto.Request.TaskItem;
using TaskManager.Application.Interfaces.Services.TaskItem;

namespace TaskManager.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskItemController(
    ITaskItemService taskItemService
) : ControllerBase
{
    [HttpPost("{ownerId:guid}")]
    public async Task<IActionResult> CreateTaskItem(Guid ownerId, TaskItemRequestDto requestDto, CancellationToken cancellationToken)
    {
        var result = await taskItemService.CreateTaskItemAsync(ownerId ,requestDto, cancellationToken);
        
        return result.IsFailure ? StatusCode(result.StatusCode, result) : Created($"/taskItem/{result.Value.Id}", result.Value);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllTaskItems(CancellationToken cancellationToken)
    {
        var result = await taskItemService.GetAllTaskItemsAsync(cancellationToken);
        
        return result.IsFailure ? StatusCode(result.StatusCode, result) : Ok(result.Value);
    }
    
    [HttpGet("{taskItemId:guid}")]
    public async Task<IActionResult> GetTaskItemById(Guid taskItemId, CancellationToken cancellationToken)
    {
        var result = await taskItemService.GetTaskItemByIdAsync(taskItemId, cancellationToken);
        
        return result.IsFailure ? StatusCode(result.StatusCode, result) : Ok(result.Value);
    }

    [HttpGet("status")]
    public async Task<IActionResult> GetTaskItemByStatus(TaskStatus taskStatus, CancellationToken cancellationToken)
    {
        var result = await taskItemService.GetTaskItemsByStatusAsync(taskStatus, cancellationToken);
        
        return result.IsFailure ? StatusCode(result.StatusCode, result) : Ok(result.Value);
    }

    [HttpPatch("update/{taskItemId:guid}")]
    public async Task<IActionResult> UpdateTaskItem(Guid taskItemId, UpdateTaskItemRequestDto requestDto,
        CancellationToken cancellationToken)
    {
        var result = await taskItemService.UpdateTaskItemAsync(taskItemId, requestDto, cancellationToken);
        
        return result.IsFailure ? StatusCode(result.StatusCode, result) : Ok(result.Value);
    }

    [HttpDelete("{taskItemId:guid}")]
    public async Task<IActionResult> DeleteTaskItem(Guid taskItemId, CancellationToken cancellationToken)
    {
        var result = await taskItemService.DeleteTaskItemByIdAsync(taskItemId, cancellationToken);
        
        return result.IsFailure ? StatusCode(result.StatusCode, result) : NoContent();
    }
}