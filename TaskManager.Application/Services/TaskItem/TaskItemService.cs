using TaskManager.Application.Dto.Request.TaskItem;
using TaskManager.Application.Dto.Response.TaskItem;
using TaskManager.Application.Interfaces.Services.TaskItem;
using TaskManager.Application.Validators.TaskItem;
using TaskManager.Application.ViewModel;
using TaskManager.Domain.Interfaces;

namespace TaskManager.Application.Services.TaskItem;

using TaskItem = TaskManager.Domain.Entities.TaskItem;


public class TaskItemService(
    ITaskItemRepository repository,
    IUserRepository repositoryUser,
    IUnitOfWork unitOfWork
) : ITaskItemService
{
    public async Task<ResponseViewModel<CreateTaskItemResponseDto>> CreateTaskItemAsync(Guid ownerId, TaskItemRequestDto requestDto, CancellationToken cancellationToken)
    {
        var validationResult = await new TaskItemCreateRequestDtoValidation().ValidateAsync(requestDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            var erros = validationResult.Errors.Select(x => x.ErrorMessage).ToList();

            return ResponseViewModel<CreateTaskItemResponseDto>.Fail(
                erros,
                400
            );
        }
        
        var owner = await repositoryUser.GetByIdAsync(ownerId, cancellationToken);

        if (owner is null)
            return ResponseViewModel<CreateTaskItemResponseDto>.Fail(
                "Owner not found",
                404
            );

        var newTask = TaskItem.Create(
            requestDto.Title,
            requestDto.Description,
            owner
        );
        
        await repository.CreateAsync(newTask, cancellationToken);
        
        await unitOfWork.CommitAsync(cancellationToken);
        
        var response = new CreateTaskItemResponseDto(newTask.Id, newTask.Title, newTask.Description, newTask.Status, owner.Id);
        
        return ResponseViewModel<CreateTaskItemResponseDto>.Ok(response);
    }

    public Task<ResponseViewModel<TaskItemResponseDto?>> GetTaskItemByIdAsync(Guid taskItemId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseViewModel<UpdateTaskItemResponseDto>> UpdateTaskItemAsync(Guid taskItemId, UpdateTaskItemRequestDto requestDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseViewModel<bool>> DeleteTaskItemByIdAsync(Guid taskItemId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseViewModel<IEnumerable<TaskItemResponseDto>>> GetAllTaskItemsAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseViewModel<IEnumerable<TaskItemResponseDto>>> GetTaskItemsByStatusAsync(TaskItemByStatusRequestDto requestDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}