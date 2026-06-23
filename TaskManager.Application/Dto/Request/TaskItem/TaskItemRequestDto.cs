namespace TaskManager.Application.Dto.Request.TaskItem;

public sealed record TaskItemRequestDto(string Title, string? Description, TaskStatus Status);