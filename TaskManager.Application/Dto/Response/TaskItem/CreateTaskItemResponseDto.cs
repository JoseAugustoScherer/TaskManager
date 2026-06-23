namespace TaskManager.Application.Dto.Response.TaskItem;

public sealed record CreateTaskItemResponseDto(Guid Id, string Title, string? Description, TaskStatus Status, Guid OwnerId);