namespace TaskManager.Application.Dto.Response.TaskItem;

public sealed record UpdateTaskItemResponseDto(Guid Id, TaskStatus Status);