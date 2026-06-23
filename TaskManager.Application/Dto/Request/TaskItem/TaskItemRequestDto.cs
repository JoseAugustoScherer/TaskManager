namespace TaskManager.Application.Dto.Request.TaskItem;

public abstract record TaskItemRequestDto(string Title, string? Description, TaskStatus Status);