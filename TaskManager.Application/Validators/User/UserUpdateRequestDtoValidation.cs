using FluentValidation;
using TaskManager.Application.Dto.Request.User;

namespace TaskManager.Application.Validators.User;

internal sealed class UserUpdateRequestDtoValidation : AbstractValidator<UpdateUserRequestDto>
{
    public UserUpdateRequestDtoValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty")
            .When(x => x.Name is not null);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty")
            .EmailAddress().WithMessage("Invalid Email format")
            .When(x => x.Email is not null);
    }
}