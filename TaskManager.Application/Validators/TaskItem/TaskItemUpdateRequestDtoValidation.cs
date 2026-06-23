using FluentValidation;
using TaskManager.Application.Dto.Request.TaskItem;

namespace TaskManager.Application.Validators.TaskItem;

internal sealed class TaskItemUpdateRequestDtoValidation : AbstractValidator<UpdateTaskItemRequestDto>
{
    public TaskItemUpdateRequestDtoValidation()
    {
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status");
    }
}