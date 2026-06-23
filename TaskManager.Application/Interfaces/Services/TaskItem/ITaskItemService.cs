using TaskManager.Application.Dto.Request.TaskItem;
using TaskManager.Application.Dto.Response.TaskItem;
using TaskManager.Application.ViewModel;

namespace TaskManager.Application.Interfaces.Services.TaskItem;

public interface ITaskItemService
{
    Task<ResponseViewModel<CreateTaskItemResponseDto>> CreateTaskItemAsync(Guid ownerId, TaskItemRequestDto requestDto, CancellationToken cancellationToken);
    Task<ResponseViewModel<TaskItemResponseDto?>> GetTaskItemByIdAsync(Guid taskItemId, CancellationToken cancellationToken);
    Task<ResponseViewModel<UpdateTaskItemResponseDto>> UpdateTaskItemAsync(Guid taskItemId, UpdateTaskItemRequestDto requestDto, CancellationToken cancellationToken);
    Task<ResponseViewModel<bool>> DeleteTaskItemByIdAsync(Guid taskItemId, CancellationToken cancellationToken);
    Task<ResponseViewModel<IEnumerable<TaskItemResponseDto>>> GetAllTaskItemsAsync(CancellationToken cancellationToken);
    Task<ResponseViewModel<IEnumerable<TaskItemResponseDto>>> GetTaskItemsByStatusAsync(TaskItemByStatusRequestDto requestDto, CancellationToken cancellationToken);

}