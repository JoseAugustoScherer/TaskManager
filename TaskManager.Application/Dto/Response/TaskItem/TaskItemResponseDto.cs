namespace TaskManager.Application.Dto.Response.TaskItem;

public sealed record TaskItemResponseDto(Guid Id, string Title, string? Description, TaskStatus Status, Guid OwnerId);