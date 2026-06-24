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
            requestDto.Status,
            owner
        );
        
        await repository.CreateAsync(newTask, cancellationToken);
        
        await unitOfWork.CommitAsync(cancellationToken);
        
        var response = new CreateTaskItemResponseDto(newTask.Id, newTask.Title, newTask.Description, newTask.Status, owner.Id);
        
        return ResponseViewModel<CreateTaskItemResponseDto>.Ok(response);
    }

    public async Task<ResponseViewModel<TaskItemResponseDto?>> GetTaskItemByIdAsync(Guid taskItemId, CancellationToken cancellationToken)
    {
        var taskItem = await repository.GetTaskItemByIdWithOwnerAsync(taskItemId, cancellationToken);

        if (taskItem is null)
            return ResponseViewModel<TaskItemResponseDto?>.Fail(
                "Task item not found",
                404
            );
        
        var response = new TaskItemResponseDto(taskItem.Id, taskItem.Title, taskItem.Description, taskItem.Status, taskItem.Owner.Id);
        
        return ResponseViewModel<TaskItemResponseDto?>.Ok(response);
    }

    public async Task<ResponseViewModel<UpdateTaskItemResponseDto>> UpdateTaskItemAsync(Guid taskItemId, UpdateTaskItemRequestDto requestDto, CancellationToken cancellationToken)
    {
        var validationResult = await new TaskItemUpdateRequestDtoValidation().ValidateAsync(requestDto, cancellationToken);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(x => x.ErrorMessage).ToList();

            return ResponseViewModel<UpdateTaskItemResponseDto>.Fail(
                errors,
                400
            );
        }
        
        var taskItem = await repository.GetByIdAsync(taskItemId, cancellationToken);

        if (taskItem is null)
            return ResponseViewModel<UpdateTaskItemResponseDto>.Fail(
                "Task item not found",
                404
            );
        
        taskItem.UpdateStatus(requestDto.Status);
        
        await repository.UpdateAsync(taskItem, cancellationToken);
        
        await unitOfWork.CommitAsync(cancellationToken);
        
        return ResponseViewModel<UpdateTaskItemResponseDto>.Ok(
            new UpdateTaskItemResponseDto(
                Id: taskItemId,
                Status: taskItem.Status
            )
        );
    }

    public async Task<ResponseViewModel<bool>> DeleteTaskItemByIdAsync(Guid taskItemId, CancellationToken cancellationToken)
    {
        var taskItem = await repository.GetByIdAsync(taskItemId, cancellationToken);

        if (taskItem is null)
            return ResponseViewModel<bool>.Fail(
                "Task item not found",
                404
            );
        
        await repository.DeleteAsync(taskItem, cancellationToken);
        
        await unitOfWork.CommitAsync(cancellationToken);
        
        return ResponseViewModel<bool>.Ok(true);
    }

    public async Task<ResponseViewModel<IEnumerable<TaskItemResponseDto>>> GetAllTaskItemsAsync(CancellationToken cancellationToken)
    {
        var taskItems = await repository.GetAllTaskItemWithOwnerAsync(cancellationToken);
        
        var taskDto = taskItems.Select(t => new TaskItemResponseDto(t.Id, t.Title, t.Description, t.Status, t.Owner.Id));
        
        return ResponseViewModel<IEnumerable<TaskItemResponseDto>>.Ok(taskDto);
    }

    public async Task<ResponseViewModel<IEnumerable<TaskItemResponseDto>>> GetTaskItemsByStatusAsync(
        TaskStatus taskStatus, CancellationToken cancellationToken)
    {
        var taskItems = await repository.GetTaskItemsByStatusWithOwnerAsync(taskStatus, cancellationToken);
        
        var taskDto = taskItems.Select(t => new TaskItemResponseDto(t.Id, t.Title, t.Description, t.Status, t.Owner.Id));
        
        return ResponseViewModel<IEnumerable<TaskItemResponseDto>>.Ok(taskDto);
    }
}