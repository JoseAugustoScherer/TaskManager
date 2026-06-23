using FluentValidation;
using TaskManager.Application.Dto.Request.TaskItem;

namespace TaskManager.Application.Validators.TaskItem;

internal sealed class TaskItemCreateRequestDtoValidation : AbstractValidator<TaskItemRequestDto>
{
    public TaskItemCreateRequestDtoValidation()
    {
        RuleFor(x => x.Title)
            .NotNull()
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(30).WithMessage("Title must not exceed 30 characters");
        
        RuleFor(x => x.Description)
            .MaximumLength(100).WithMessage("Description must not exceed 100 characters")
            .When(x => x.Description is not null);
        
        RuleFor(x => x.Status)
            .IsInEnum().WithMessage("Invalid status");
    }
}